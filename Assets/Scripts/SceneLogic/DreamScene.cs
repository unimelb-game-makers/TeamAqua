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
    private DialogueManager dialogueManager;
    public string BG_MUSIC_1 = "BGM_A_DREAM";
    public string PREV_MUSIC = "BGM_CUTSCENE_TRANSFORMATION";

    [Header("Loading")]
    [SerializeField]
    private PlayerSave playerSave;

    private void Awake()
    {
        InitManagers();
        InitUI();
        LoadData();
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
    }

    private void LoadData()
    {
        playerSave.Load();
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
