using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Step", fileName = "Quest Step")]
public class QuestStep : ScriptableObject
{
    public QuestType type = QuestType.Gather;
    [TextArea]
    public string description = string.Empty;
    [ShowIf("type", QuestType.Gather)] 
    public List<QuestItem> requiredItems = new();
    [ShowIf("type", QuestType.Location)] 
    public string locationId = string.Empty;
    [ShowIf("type", QuestType.Talk)] 
    public string npcId = string.Empty;

    public void Resolve()
    {
        switch (type)
        {
            case QuestType.Gather:
                for (int i = 0; i < requiredItems.Count; ++i)
                {
                    if (!InventoryManager.instance.HasItem(requiredItems[i].item.name, requiredItems[i].amount))
                        throw new InvalidOperationException($"QUEST | Could not resolve {name} due to lack of items");
                    InventoryManager.instance.SubtractItem(requiredItems[i].item, requiredItems[i].amount);
                }
                break;
        }
    }
}
