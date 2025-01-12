using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class All_UI_Off_State: UIState
{
    [SerializeField] UIState inventoryState;
    [SerializeField] UIState questState;
    [SerializeField] UIState journalState;
    [SerializeField] UIState dialogueState;

    public UIState PauseOn;
    /*
    [SerializeField] public UIState questOn;
    // [SerializeField] public UIState MapOn;
    [SerializeField] public UIState JournalOn;
    */
    public override void UIEnter()
    {


        /* All UI states turned off when entering this state, following inv state, all other states should be child of UIstatemachine, with UI elements attached under their respective manager. Unsure how to proceed with dialogue stuffs, very chaotic.
        */



        Debug.Log("Entering all UI off state");
        
        //Make sure all states are turned off

        // Time resumes
        Time.timeScale = 1;
    }

    public override void UIProcess()
    {   // i dont know a better way to access list in c# than indexing tbh.....
        if (Input.GetKeyDown(KeyCode.I) && !DialogueSystem.GetIsPlaying())
        {   //inventory
            UIstatemachine.ChangeUIState(inventoryState);
            Debug.Log("Going into inventory system");
        }
        
        if (Input.GetKeyDown(KeyCode.J) && !DialogueSystem.GetIsPlaying())
        {   //quest
            UIstatemachine.ChangeUIState(questState);
        }
        /*
        if (Input.GetKeyDown(KeyCode.H) && !DialogueSystem.GetIsPlaying())
        {   //journal
            UIstatemachine.ChangeUIState(journalState);
        }   */

        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueSystem.GetIsPlaying())
        {   //pause
            //this.UIEnter();
            UIstatemachine.ChangeUIState(PauseOn);
        }

        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            UIstatemachine.ChangeUIState(MapOn);
        }
        */
    }

    public override void UIExit()
    {
        Debug.Log("Exiting ALL UI OFF state");
    }
    /*
        
    }

    private void OnEnableI()
    {
        //menuAction.Enable();
        UpdateSlots();
        inventoryMenu.SetActive(true);
        _menuActivated = true;
        UIinputProvider.instance().SendUIinput(1);
    }

    private void OnDisableI()
    {
        //menuAction.Disable();
        inventoryMenu.SetActive(false);
        _menuActivated = false;
        UIinputProvider.instance().SendUIinput(0);
    }
   */
}