using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerPoint : MonoBehaviour
{
    /*
    THE BIG IDEA: same logic as other mode of dialogue, but instead of every key E, its every time u collide with a trigger point
    this script is attached to a parent that takes in all the dialogue trigger points as children

    big question: can we detect collision without having to add a script to every single child object, can all that logic be done here instead?
    if not, maybe think of scriptable objects for these trigger points? but the data should be static though.

    =============================================================================================================================

    this works for now, next is to check story end, decide when to call exitdialoguemode, 
    */
    public UIState All_UI_Off;
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
    public void EnterDialogue(){
        if (collided) return;

        Debug.Log("Entering dialogue trigger point child mode");
        DialogueManager.GetIsPlaying();
        //UIEnter();    < what did this line do....
        DialogueManager.instance.ContinueStory();
        Debug.Log("entering dial trig ui state");
        //UIstatemachine.ChangeUIState(this);
        //Collided = false;
    }
    public void ExitDialogue(){
        //UIstatemachine.ChangeUIState(All_UI_Off);
        collided = true;    // remembers that this trig point has already been collided
        Debug.Log("exiting child dialogue triggers");
        this.gameObject.SetActive(false);
    }
}
