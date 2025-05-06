using Popups;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [Header("Level")]
    [SerializeField]
    private UI ui;

    [Header("Managers")]
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private SpriteManager spriteManager;

    [Header("Level")]
    [SerializeField]
    private Level level;

    private void Awake()
    {
        InitUI();
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

    private void InitManagers()
    {
        GameObject manager = new("Managers");
        DontDestroyOnLoad(manager);
        Game.AddManager(Instantiate(audioManager, manager.transform));
        Game.AddManager(Instantiate(dialogueManager, manager.transform));
        Game.AddManager(Instantiate(spriteManager, manager.transform));
    }

    /// <summary>
    /// Simply creates the level.
    /// </summary>
    private void InitLevel()
    {
        Instantiate(level);
    }
}
