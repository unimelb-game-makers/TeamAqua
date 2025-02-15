using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized] public NPCDialogue dialogueSource = null;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueSource != null && !DialogueSystem.GetIsPlaying())
        {
            dialogueSource.PlayDialogue();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Creature")) return;
        if (other.gameObject.TryGetComponent(out NPC npc) && npc.dialogue)
        {
            dialogueSource = npc.dialogue;
            if(dialogueSource.HasQuest)
                dialogueSource.IndicateQuest();
            else
                dialogueSource.IndicateDialogue();
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
