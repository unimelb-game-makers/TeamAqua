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
            //UIstatemachine.ChangeUIState(All_UI_Off);
            UIstatemachine.ChangeUIState(paused);
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