using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class DayStoryManager : MonoBehaviour
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

    public void GetNextDay()
    {
        // set day to the next one
        currentDay += 1;
    }

    public void setNextDialoguePool()
    {
        // set next dialogue
        dialogueManager.dialogueDatabase = dayDatabase.days[currentDay].dialoguePool;
    }

    public void setNextQuests()
    {
        //================ WIP ================

        // set next quests and subquests
        // questManager.Quests = dayDatabase.days[currentDay].mainQuests;
        // questManager.Quests = dayDatabase.days[currentDay].subQuests;
    }
}
