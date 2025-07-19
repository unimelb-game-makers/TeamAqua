using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest", fileName = "Quest")]
public class Quest : DialogueNode
{
    public string title;

    [TextArea]
    public string description;

    [InlineEditor]
    public List<QuestStep> steps = new List<QuestStep>();
    public QuestReward reward;

    public QuestStep GetStep(string id)
    {
        for (int i = 0; i < steps.Count; ++i)
        {
            if (steps[i].name == id)
                return steps[i];
        }

        throw new KeyNotFoundException($"QUEST | Could not find Quest Step '{id}'");
    }
}
