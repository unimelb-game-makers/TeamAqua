using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public enum QuestState
{
    // Hasn't been picked up yet
    Ready,

    //in the Quest Node
    TakeQuest,

    // Ongoing means there are still things that can be done
    Ongoing,

    // Finished means that it has been completed, but hasn't been submitted
    Completed,

    // Submitted means that it has been fully sent through
    Submitted,
}

[Serializable]
public class QuestStepTracker
{
    public QuestStep step;
    public QuestState state = QuestState.Ready;
}

[Serializable]
public class QuestTracker
{
    public Quest quest;
    public List<QuestStepTracker> steps = new();
    public QuestState state = QuestState.Ready;
}

public class QuestManager : MonoBehaviour, ISaveable
{
    // Singleton instance
    public static QuestManager instance;

    [SerializeField]
    private QuestDatabase questDatabase;

    [NonSerialized, ShowInInspector, ReadOnly]
    private List<QuestTracker> _quests = new(); // list of all the quests the player has

    public List<QuestTracker> Quests => _quests;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Load(SaveSlot saveSlot)
    {
        JournalSaveData saveData = saveSlot.journalSaveData;
        if (saveData.quests == null)
            return;
        QuestSaveData[] questSaves = saveData.quests;
        for (int i = 0; i < questSaves.Length; ++i)
        {
            QuestTracker questTracker = new();
            _quests.Add(questTracker);
            Quest quest = questDatabase.GetQuest(questSaves[i].id);
            questTracker.quest = quest;
            questTracker.state = questSaves[i].state;
            if (questSaves[i].steps == null)
                continue;
            for (int j = 0; j < questSaves[i].steps.Length; ++j)
            {
                QuestStepTracker stepTracker = new();
                questTracker.steps.Add(stepTracker);
                stepTracker.step = quest.GetStep(questSaves[i].steps[j].id);
                stepTracker.state = questSaves[i].steps[j].state;
            }
        }
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        QuestSaveData[] questSaves = new QuestSaveData[_quests.Count];
        for (int i = 0; i < _quests.Count; ++i)
        {
            questSaves[i].id = _quests[i].quest.name;
            questSaves[i].state = _quests[i].state;
            questSaves[i].steps = new QuestStepSaveData[_quests[i].steps.Count];
            for (int j = 0; j < _quests[i].steps.Count; ++j)
            {
                questSaves[i].steps[j].id = _quests[i].steps[j].step.name;
                questSaves[i].steps[j].state = _quests[i].steps[j].state;
            }
        }

        save.journalSaveData.quests = questSaves;
        return save;
    }

    public void AddQuest(string questId)
    {
        Quest quest = questDatabase.GetQuest(questId);
        AddQuest(quest);
    }

    public void AddQuest(Quest quest)
    {
        // Don't add if there is a duplicate
        for (int i = 0; i < _quests.Count; ++i)
        {
            if (_quests[i].quest == quest)
            {
                throw new InvalidOperationException(
                    $"QUEST | Trying to add Quest {quest.name}, but it has already been added"
                );
            }
        }
        QuestTracker questTracker = new();
        questTracker.quest = quest;
        for (int i = 0; i < quest.steps.Count; ++i)
        {
            QuestStepTracker step = new();
            step.step = quest.steps[i];
            questTracker.steps.Add(step);
        }
        _quests.Add(questTracker);
        questTracker.state = QuestState.Ongoing; //the moment u take a quest, it becomes ongoing
    }

    public bool IsSubmitted(string questId)
    {
        if (TryGetActiveQuest(questId, out QuestTracker tracker))
            return tracker.state == QuestState.Submitted;
        return false;
    }

    private bool TryGetActiveQuest(string questId, out QuestTracker tracker)
    {
        for (int i = 0; i < _quests.Count; ++i)
        {
            if (_quests[i].quest.name == questId)
            {
                tracker = _quests[i];
                return true;
            }
        }

        tracker = null;
        return false;
    }

    public QuestState CheckQuest(string questId)
    {
        if (TryGetActiveQuest(questId, out QuestTracker tracker))
        {
            UpdateState(tracker);
            Debug.Log("tracker.state is: " + tracker.state);
            return tracker.state;
        }

        return QuestState.Ready;
    }

    private void UpdateState(QuestTracker tracker)
    {
        // Don't bother if it has already been submitted
        //if (tracker.state != QuestState.Submitted || tracker.state != QuestState.Completed)
        //{        }
        if (tracker.state == QuestState.Submitted)
            return;
        // if (tracker.state == QuestState.TakeQuest)
        // {
        //     return;
        // }
        QuestState state = QuestState.Completed;
        for (int i = 0; i < tracker.steps.Count; ++i)
        {
            QuestStepTracker stepTracker = tracker.steps[i];
            switch (stepTracker.step.type)
            {
                case QuestType.Gather:
                    List<QuestItem> requiredItems = stepTracker.step.requiredItems;
                    for (int j = 0; j < requiredItems.Count; ++j)
                    {
                        bool hasItem = InventoryManager.instance.HasItem(
                            requiredItems[j].item.name,
                            requiredItems[j].amount
                        );
                        if (!hasItem)
                            state = QuestState.Ongoing;
                    }
                    break;
                // If location and talk are still ongoing, then the quest is still ongoing
                case QuestType.Action:
                    if (
                        stepTracker.state == QuestState.Ongoing
                        || stepTracker.state == QuestState.Ready
                    )
                        state = QuestState.Ongoing;
                    break;
            }
        }

        tracker.state = state;
    }

    public void CompleteStep(QuestStep step)
    {
        // Go through all the possible step ids and mark it as completed

        /*
        REFRACTOR THIS SO YOU DONT HAVE TO BE IN A QUEST NODE TO COMPLETE a quest
        */
        for (int i = 0; i < _quests.Count; ++i)
        {
            if (_quests[i].state != QuestState.Ongoing)
            {
                Debug.Log("ONGOING | QuestState is: " + _quests[i].state);
                continue;
            }

            // if (_quests[i].state == QuestState.Ready)
            // {
            //     Debug.Log("ONGOING | QuestState is: " + _quests[i].state);
            //     continue;
            // }

            for (int j = 0; j < _quests[i].steps.Count; ++j)
            {
                if (_quests[i].steps[j].step == step)
                {
                    _quests[i].steps[j].state = QuestState.Completed;
                    Debug.Log("CompleteStep: QuestState is: " + _quests[i].steps[j].state);
                }
                Debug.Log(
                    "CompleteStep (out side if): QuestState is: " + _quests[i].steps[j].state
                );
            }
            //CheckQuest(something something string id);
            //UpdateState(_quests[i]);
        }
    }

    public void SubmitQuest(string questId)
    {
        if (!TryGetActiveQuest(questId, out QuestTracker tracker))
            return;
        for (int i = 0; i < tracker.steps.Count; ++i)
        {
            tracker.steps[i].step.Resolve();
            tracker.steps[i].state = QuestState.Submitted;
        }

        tracker.state = QuestState.Submitted;
    }

    public void CompletePuzzleStep(string puzzleId) { }
}
