using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class All_UI_Off_State: UIState
{
    [SerializeField] 
    public List<UIState> StatesList;
    public List<GameObject> UI_canvases;
    /*
    [SerializeField] public UIState questOn;
   // [SerializeField] public UIState MapOn;
    [SerializeField] public UIState JournalOn;
    */
    public override void UIEnter()
    {

        foreach ( GameObject canvas in UI_canvases)
        {   // takes in list of all UI canvases and set them to inactive
            canvas.SetActive(false);
        }
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {   //inventory
            UIstatemachine.ChangeUIState(StatesList[0]);
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {   //quest
            UIstatemachine.ChangeUIState(StatesList[1]);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {   //journal
            UIstatemachine.ChangeUIState(StatesList[2]);
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