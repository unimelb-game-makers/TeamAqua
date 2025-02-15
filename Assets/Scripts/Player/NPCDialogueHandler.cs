using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized] public NPCDialogue dialogueSource = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueSource != null && !DialogueSystem.GetIsPlaying() && !UIStatemachine.uiStatemachine.CheckPause())
        {
            dialogueSource.PlayDialogue();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Creature")){
            dialogueSource = other.gameObject.GetComponent<NPC>().dialogue;
            
            if(dialogueSource.HasQuest){
                dialogueSource.IndicateQuest();
            } else{
                dialogueSource.IndicateDialogue();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Creature") && dialogueSource != null)
        {
            dialogueSource.HideIndicators();
            dialogueSource = null;
        }
    }
}
