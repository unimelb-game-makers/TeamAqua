using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class All_UI_Off_State: UIState
{
    [SerializeField] 
    public List<UIState> StatesList;
    public UIState PauseOn;
    public List<GameObject> UI_canvases;
    /*
    [SerializeField] public UIState questOn;
   // [SerializeField] public UIState MapOn;
    [SerializeField] public UIState JournalOn;
    */
    public override void UIEnter()
    {
        Debug.Log("Entering all UI off state");
        foreach ( GameObject canvas in UI_canvases)
        {   // takes in list of all UI canvases and set them to inactive
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
                Time.timeScale = 1;     // time resumes
                break;  //break out of loop if found the active UI
            }
            
        }
    }

    public override void UIProcess()
    {   // i dont know a better way to access list in c# than indexing tbh.....
        if (Input.GetKeyDown(KeyCode.I) && !DialogueSystem.GetIsPlaying())
        {   //inventory
            UIstatemachine.ChangeUIState(StatesList[0]);
        }
        
        if (Input.GetKeyDown(KeyCode.J) && !DialogueSystem.GetIsPlaying())
        {   //quest
            UIstatemachine.ChangeUIState(StatesList[1]);
        }

        if (Input.GetKeyDown(KeyCode.H) && !DialogueSystem.GetIsPlaying())
        {   //journal
            UIstatemachine.ChangeUIState(StatesList[2]);
        }

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