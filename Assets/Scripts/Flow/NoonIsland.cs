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

    [SerializeField]
    private Vector3 spawnPosition;

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

    [Header("Level")]
    [SerializeField]
    private Level noonIslandLevel;

    private void Awake()
    {
        InitUI();
        InitPlayer();
        InitManagers();
        InitLevel();
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

        player.transform.position = spawnPosition;
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
        Instantiate(barrierManager, manager.transform);
        Instantiate(audioManager, manager.transform);
        Instantiate(inventoryManager, manager.transform);
        Instantiate(energyManager, manager.transform);
        Instantiate(questManager, manager.transform);
        Instantiate(dialogueManager, manager.transform);
        Instantiate(spriteManager, manager.transform);
        Instantiate(dayManager, manager.transform);
        Instantiate(waterManager, manager.transform);
    }

    /// <summary>
    /// Simply creates the level.
    /// </summary>
    private void InitLevel()
    {
        SceneManager.LoadScene("NoonIslandEnvironment", LoadSceneMode.Additive);
        Instantiate(noonIslandLevel); //jumps into dialogue upon entering
    }
}
