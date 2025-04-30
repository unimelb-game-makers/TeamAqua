using UnityEngine;
using Popups;

public class GameFlow : MonoBehaviour
{
    [Header("Managers")] 
    [SerializeField] private BarrierManager barrierManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] private DayManager dayManager;
    [SerializeField] private WaterManager waterManager;

    [Header("Level")] [SerializeField] private Level level;

    [Header("UI")] [SerializeField] private UI ui;

    private void Awake()
    {
        InitUI();
        InitManagers();
        InitLevel();
    }

    private void InitUI()
    {
        Instantiate(ui);
    }
    
    /// <summary>
    /// Spawn the managers in a deterministic order so that we know what is loaded when.
    /// We want complete control over each of their flows.
    /// </summary>
    private void InitManagers()
    {
        GameObject manager = new GameObject("Managers");
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

    private void InitLevel()
    {
        Instantiate(level);
    }
}
