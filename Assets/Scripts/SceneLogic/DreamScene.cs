using System;
using System.Collections;
using System.Collections.Generic;
using Popups;
using UnityEngine;

public class DreamScene : MonoBehaviour
{
    [Header("Level")]
    [SerializeField]
    private UI ui;

    [Header("Managers")]
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private PrefabManager prefabManager;
    
    [Header("Dialogue")] 
    [SerializeField]
    private DialogueScript dreamScript;
    
    [SerializeField]
    private DialogueNode dreamNode;

    [SerializeField]
    private DialogueManager dialogueManager;
    public string BG_MUSIC_1 = "BGM_A_DREAM";
    public string PREV_MUSIC = "BGM_CUTSCENE_TRANSFORMATION";

    private void Awake()
    {
        InitManagers();
        InitUI();
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
        Game.AddManager(Instantiate(prefabManager, manager.transform));
        DialogueManager.instance.SetDialogue(dreamScript, dreamNode);
    }

    private void Start()
    {
        AudioManager.Instance.Stop(PREV_MUSIC);
        AudioManager.Instance.Play(BG_MUSIC_1);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.Stop(BG_MUSIC_1);
    }
}
