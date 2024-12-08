using UnityEngine;
using UnityEngine.UI;

public class InventoryOn : UIState
{
    [SerializeField] public UIState All_UI_Off;
    public UIState paused;
    [SerializeField] GameObject inventoryMenu;
    public override void UIEnter()
    {
        Debug.Log("entering inventory on state");
        //menuAction.Enable();
        InventoryManager.instance().UpdateSlots();
        inventoryMenu.SetActive(true);
        
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
            
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
            UIstatemachine.ChangeUIState(paused);
        }


        if (DialogueSystem.GetIsPlaying()) // forcibly closes inventory if player enters dialogue
        {
            UIstatemachine.ChangeUIState(All_UI_Off /*UI_Off_State*/);
            // ->>> solution: make a new Ui_Off_State to handle all inputs for moving to xxxOn state and turning said state off
        }
        InventoryManager.instance().UpdateSlots();
    }

    public override void UIExit()
    {
        Debug.Log("Exiting InventoryOn state");
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