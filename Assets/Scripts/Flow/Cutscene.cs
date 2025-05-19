using Popups;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [Header("UI")]
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
        
        // We want to reset the dialogue in case the player starts again from the same game
        // This is because we don't call PlayerSave.Load()
        DialogueManager.instance.ResetDialogue();
    }

    /// <summary>
    /// Simply creates the level.
    /// </summary>
    private void InitLevel()
    {
        Instantiate(level);
    }
}
