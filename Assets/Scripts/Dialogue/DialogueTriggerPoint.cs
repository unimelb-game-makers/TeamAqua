using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerPoint : MonoBehaviour
{
    public List<DialogueNode> dialogues;
    public bool collided = false;   // to record which trig point has alr been hit, avoiding the same trig point activating multiple times

//================================ honestly not sure what to do with this state ==========================================
    // private bool TryGetTrigger(string dialogueId, out DialogueTrigger trigger)
    // {
    //     for (int i = 0; i < npcData.dialogues.Count; ++i)
    //     {
    //         if (npcData.dialogues[i].dialogue.name == dialogueId)
    //         {
    //             trigger = npcData.dialogues[i];
    //             return true;
    //         }
    //     }

    //     trigger = null;
    //     return false;
    // }

    // public bool PlayDialogue()
    // {
    //     string dialogue = DialogueManager.instance.DialogueId;
    //     if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
    //     {
    //         DialogueManager.instance.EnterDialogue(trigger.dialogue, trigger.mode);
    //         return true;
    //     }

    //     return false;
    // }
    /*
    This will be automatically called on Trigger by NPCDialogueHandler
    
    */
    public void EnterDialogue(){
        if (collided) return;
        Debug.Log("Entering dialogue trigger point child mode");
        DialogueManager.instance.EnterDialogue(dialogues[0], DialogueMode.Moving);
        // DialogueManager.GetIsPlaying();
        // DialogueManager.instance.ContinueStory();
        // Debug.Log("entering dial trig ui state");

    }

    public void ExitDialogue(){
        collided = true;    // remembers that this trig point has already been collided
        Debug.Log("exiting child dialogue triggers");
        this.gameObject.SetActive(false);
    }
}
