using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UI;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized] 
    public NPCDialogue dialogueSource = null;
    [SerializeField]
    public NPCTag dialogueIcons;        //there are going to be many dialogue tags
    [SerializeField] 
    public UIController UIcontroller = null;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueSource != null && !DialogueSystem.GetIsPlaying() && !UIcontroller.pausePopup.isShowing /*&& not paused*/)
        {
            dialogueSource.PlayDialogue();
            dialogueIcons.AttachStory();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Creature")) return;
        dialogueIcons.CheckTag();
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
