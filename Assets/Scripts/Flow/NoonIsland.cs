using Cinemachine;
using Popups;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class NoonIsland : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private UI ui;

    [Header("Cameras")]
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [Header("Player")]
    [SerializeField]
    private PlayerController playerController;

    [Header("Managers")]
    [SerializeField]
    private BarrierManager barrierManager;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private EnergyManager energyManager;

    [SerializeField]
    private QuestManager questManager;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private SpriteManager spriteManager;

    [SerializeField]
    private DayManager dayManager;

    [SerializeField]
    private WaterManager waterManager;

    [SerializeField]
    private CameraManager cameraManager;

    [Header("Level")]
    [SerializeField]
    private Level noonIslandLevel;

    [Header("Loading")]
    [SerializeField] private PlayerSave playerSave;

    private void Awake()
    {
        InitUI();
        InitPlayer();
        InitManagers();
        InitLevel();
        LoadData();
    }

    /// <summary>
    /// Simply creates the UI.
    /// </summary>
    private void InitUI()
    {
        Instantiate(ui);
    }

    /// <summary>
    /// Spawns the player, then makes the camera look at them.
    /// </summary>
    private void InitPlayer()
    {
        Camera _ = Instantiate(mainCamera);
        CinemachineVirtualCamera virtualCam = Instantiate(virtualCamera);
        PlayerController player = Instantiate(playerController);

        virtualCam.LookAt = player.transform;
        virtualCam.Follow = player.transform;
    }

    /// <summary>
    /// Spawn the managers in a deterministic order so that we know what is loaded when.
    /// We want complete control over each of their flows.
    /// </summary>
    private void InitManagers()
    {
        GameObject manager = new("Managers");
        DontDestroyOnLoad(manager);
        Game.AddManager(Instantiate(barrierManager, manager.transform));
        Game.AddManager(Instantiate(audioManager, manager.transform));
        Game.AddManager(Instantiate(inventoryManager, manager.transform));
        Game.AddManager(Instantiate(energyManager, manager.transform));
        Game.AddManager(Instantiate(questManager, manager.transform));
        Game.AddManager(Instantiate(dialogueManager, manager.transform));
        Game.AddManager(Instantiate(spriteManager, manager.transform));
        Game.AddManager(Instantiate(dayManager, manager.transform));
        Game.AddManager(Instantiate(waterManager, manager.transform));
        Game.AddManager(Instantiate(cameraManager, manager.transform));
    }

    /// <summary>
    /// Simply creates the level.
    /// </summary>
    private void InitLevel()
    {
        SceneManager.LoadScene("NoonIslandEnvironment", LoadSceneMode.Additive);
        Instantiate(noonIslandLevel); //jumps into dialogue upon entering
    }

    private void LoadData()
    {
        playerSave.Load();
    }
}
