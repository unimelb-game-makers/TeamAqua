using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest quest;

    public void GiveQuest()
    {
        QuestManager.instance.AddQuest(quest);
    }
}
