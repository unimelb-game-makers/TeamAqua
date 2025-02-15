using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UNUSED ----> GO TO DialogueTriggerPoints.cs
//Not sure if should inehrit from UIstate or should just make dialogue states have their own class
public class DialogueGameplay : UIState
{
    [SerializeField] public UIState All_UI_Off;
    [SerializeField] public GameObject DialoguePanel;
    public DialogueTriggerPoints triggers;


//================================ honestly not sure what to do with this state ==========================================
// WHAT IF:
/* 
using UItriggerenter and UItriggerexit to go in and out of dialogue state,
in parent game object collider, call enter dialogue mode, parent takes the ink json file
each time hit a collider, enter state (exit collider calls trig exit)
each time state is called, call continuestory()

*/
    public override void UIEnter()
    {
        Debug.Log("Entering dialogue mode");
        
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
            StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
            
        }
    }

    public override void UIExit()
    {
        StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
        Debug.Log("exiting dialogue mode");
    }




}
