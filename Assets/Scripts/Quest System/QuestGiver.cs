using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private int questID; // the ID of the quest that this quest giver gives
    
    public void GiveQuest()
    {
        QuestManager.instance.AddQuest(questID);
    }
}
