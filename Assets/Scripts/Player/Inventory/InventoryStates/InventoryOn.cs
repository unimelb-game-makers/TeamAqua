using UnityEngine;
using UnityEngine.UI;

public class InventoryOn : UIState
{
    [SerializeField] public UIState inventoryOff;
    [SerializeField] GameObject inventoryMenu;
    private bool _menuActivated;
    public override void UIEnter()
    {
        Debug.Log("entering inventory on state");
        //menuAction.Enable();
        InventoryManager.instance().UpdateSlots();
        inventoryMenu.SetActive(true);
        _menuActivated = true;
        
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIstatemachine.ChangeUIState(inventoryOff);
        }

        if (DialogueSystem.GetIsPlaying() || PausePanelScript.instance().isPaused) // forcibly closes inventory if player enters dialogue
        {
            UIstatemachine.ChangeUIState(inventoryOff);
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