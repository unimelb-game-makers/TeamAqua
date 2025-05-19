using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    private QuestStep questStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            QuestManager.instance.CompleteStep(questStep);
            Debug.Log("shit gone wrong");
        }
    }
}
