using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Quests/Quest Database", fileName = "Quest Database")]
public class QuestDatabase : ScriptableObject
{
    [InlineEditor] public List<Quest> quests;

    public Quest GetQuest(string id)
    {
        for (int i = 0; i < quests.Count; ++i)
        {
            if (quests[i].name == id)
                return quests[i];
        }

        throw new KeyNotFoundException($"QUEST | Could not find Quest '{id}'");
    }
}
