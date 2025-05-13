using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A strictly developer-only scriptable object that should be set in replacement of a save slot.
/// </summary>
[CreateAssetMenu(fileName = "Save Template", menuName = "ScriptableObjects/Save Template")]
public class SaveTemplate : ScriptableObject
{
    [TextArea]
    public string description;
    [SerializeField] private Checkpoint checkpoint;
    [SerializeField] private DialogueScript script;
    [SerializeField] private DialogueNode dialogue;
    [SerializeField] private List<Quest> ongoingQuests;
    [SerializeField] private int energy = 100;
    [SerializeField] private int day = 1;

    public SaveSlot CreateSaveSlot()
    {
        SaveSlot save = new ();
        save.playerSaveData.position = checkpoint.position;
        save.playerSaveData.energy = energy;
        save.worldSaveData.currentDay = day;
        save.dialogueSaveData.scriptId = script.name;
        save.dialogueSaveData.dialogueId = dialogue.name;
        QuestSaveData[] quests = new QuestSaveData[ongoingQuests.Count];
        for(int i=0; i< quests.Length; ++i)
        {
            quests[i].id = ongoingQuests[i].name;
            quests[i].state = QuestState.Ongoing;
            QuestStepSaveData[] steps = new QuestStepSaveData[ongoingQuests[i].steps.Count];
            for (int j = 0; j < steps.Length; ++j)
            {
                steps[j].id = ongoingQuests[i].steps[j].name;
                steps[j].state = QuestState.Ongoing;
            }

            quests[i].steps = steps;
        }

        save.journalSaveData.quests = quests;
        return save;
    }
}