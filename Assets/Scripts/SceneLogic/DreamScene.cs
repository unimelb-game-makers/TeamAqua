using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamScene : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private DialogueManager dialogueManager;
    public string BG_MUSIC_1 = "BGM_A_DREAM";

    private void Awake()
    {
        GameObject manager = new("Managers");
        DontDestroyOnLoad(manager);
        Game.AddManager(Instantiate(audioManager, manager.transform));
        Game.AddManager(Instantiate(dialogueManager, manager.transform));
    }

    private void Start()
    {
        AudioManager.Instance.Play(BG_MUSIC_1);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.Stop(BG_MUSIC_1);
    }
}
