using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Not sure if should inehrit from UIstate or should just make dialogue states have their own class
public class DialogueGameplay : UIState
{
    [SerializeField] public UIState All_UI_Off;
    [SerializeField] public GameObject DialoguePanel;
    public DialogueTriggerPoints triggers;


//================================ honestly not sure what to do with this state ==========================================
// WHAT IF:
/*

*/
    public override void UIEnter()
    {
        Debug.Log("Entering dialogue mode");
        if (!DialogueSystem.GetIsPlaying())
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
        }
        All_UI_Off.UIEnter();
        DialoguePanel.SetActive(true);
        DialogueSystem.GetIsPlaying();
    }
    public override void UIProcess()
    {
        /*  
        if (triggers.collided////// or a function call here (dialogue trig is collided))
        {
            DialogueSystem.GetDial().ContinueStory();
            Debug.Log("story is continued");
        } 
        
        check if a collider has already been collided? bool array and then reset all bools to false on UIexit???

        When to exit?: right now, once dialogue ends (logic in continuestory), it auto triggers exitdialoguemode and that func enters alluioff state, which should work 
        
        */ 

        if (Input.GetKeyDown(KeyCode.Escape) && DialogueSystem.GetIsPlaying())
        {
            StartCoroutine(DialogueSystem.GetDial().ExitDialogueMode());
            
        }
    }

    public override void UIExit()
    {
        StartCoroutine(DialogueSystem.GetDial().ExitDialogueMode());
        Debug.Log("exiting dialogue mode");
    }




}
