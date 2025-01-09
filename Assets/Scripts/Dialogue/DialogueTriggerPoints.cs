using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerPoints : UIState
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
    [SerializeField] public GameObject DialoguePanel;
    public bool Collided = false;   // to record which trig point has alr been hit, avoiding the same trig point activating multiple times





//================================ honestly not sure what to do with this state ==========================================

    public override void UIEnter()
    {
        Debug.Log("Entering dialogue trigger point child mode");
        DialoguePanel.SetActive(true);
        DialogueSystem.GetIsPlaying();
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            if (!Collided)
            {
                UIEnter();
                DialogueSystem.GetDial().ContinueStory();
                Debug.Log("entering dial trig ui state");
                UIstatemachine.ChangeUIState(this);
                //Collided = false;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            UIstatemachine.ChangeUIState(All_UI_Off);
            Collided = true;    // remembers that this trig point has already been collided
        }
    }

    public override void UIExit()
    {
        this.gameObject.SetActive(false);
        Debug.Log("exiting child dialogue triggers");
    }
}
