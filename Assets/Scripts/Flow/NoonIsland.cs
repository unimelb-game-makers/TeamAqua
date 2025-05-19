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

    [SerializeField]
    private SaveManager saveManager;

    [Header("Level")]
    [SerializeField]
    private Level noonIslandLevel;

    [Header("Loading")]
    [SerializeField]
    private PlayerSave playerSave;

    private Transform _managers;

    private void Awake()
    {
        InitHolder();
        InitUI();
        InitPlayer();
        InitManagers();
        InitLevel();
        LoadData();
    }

    private void InitHolder()
    {
        GameObject managers = new ("Managers");
        _managers = managers.transform;
        DontDestroyOnLoad(_managers);
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
        PlayerController player = Instantiate(playerController);
        Game.AddManager(Instantiate(cameraManager, _managers));
        CameraManager.instance.SetPlayer(player);
    }

    /// <summary>
    /// Spawn the managers in a deterministic order so that we know what is loaded when.
    /// We want complete control over each of their flows.
    /// </summary>
    private void InitManagers()
    {
        Game.AddManager(Instantiate(barrierManager, _managers));
        Game.AddManager(Instantiate(audioManager, _managers));
        Game.AddManager(Instantiate(inventoryManager, _managers));
        Game.AddManager(Instantiate(energyManager, _managers));
        Game.AddManager(Instantiate(questManager, _managers));
        Game.AddManager(Instantiate(dialogueManager, _managers));
        Game.AddManager(Instantiate(spriteManager, _managers));
        Game.AddManager(Instantiate(dayManager, _managers));
        Game.AddManager(Instantiate(waterManager, _managers));
        // Save Manager is special as it is present in the first scene and isn't added to the _manager pool.
        Instantiate(saveManager, _managers);
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
        SaveManager.instance.Load();
    }
}
