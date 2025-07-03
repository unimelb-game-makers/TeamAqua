using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class DayStoryManager : MonoBehaviour, ISaveable
{
    public static DayStoryManager instance;

    [SerializeField]
    [InlineEditor]
    public DayDatabase dayDatabase;
    public int currentDay;

    [SerializeField]
    public DialogueManager dialogueManager;

    [SerializeField]
    public QuestManager questManager;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        currentDay = 0;
        return;
    }

    public void Load(SaveSlot saveSlot)
    {
        // _currentDay = saveSlot.worldSaveData.currentDay;
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        // save.worldSaveData.currentDay = _currentDay;
        return save;
    }

    public void GetNextDay(int currentDay)
    {
        // set day index to start at 0
        currentDay -= 1;

        // set next dialogue pool
        SetNextDialoguePool(currentDay);
    }

    public void SetNextDialoguePool(int currentDay)
    {
        // set next dialogue

        // NOTE: this currently fails to switch the dialogue pool at all
        Debug.Log("currenrt day is: " + currentDay);
        dialogueManager.dialogueDatabase = dayDatabase.days[currentDay].dialoguePool;
        Debug.Log("current dialogue pool is: " + dayDatabase.days[currentDay].dialoguePool.name);

        // UNSURE: do we need to close off any ongoing dialogue nodes and quests prior to switching?
    }

    public void setNextQuests()
    {
        //================ WIP ================

        // set next quests and subquests
        // questManager.Quests = dayDatabase.days[currentDay].mainQuests;
        // questManager.Quests = dayDatabase.days[currentDay].subQuests;
    }
}
