using UnityEngine;
using UnityEngine.UI;

public class InventoryOff : UIState
{
    [SerializeField] public UIState inventoryOn;
    [SerializeField] GameObject inventoryMenu;
    private bool _menuActivated;
    public override void UIEnter()
    {
        Debug.Log("entering inventoryOff state");
        //menuAction.Disable();
        inventoryMenu.SetActive(false);
        _menuActivated = false;
        
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIstatemachine.ChangeUIState(inventoryOn);
        }

        if (DialogueSystem.GetIsPlaying() || PausePanelScript.instance().isPaused) // forcibly closes inventory if player enters dialogue
        {
            UIstatemachine.ChangeUIState(inventoryOn);
        }
        //UpdateSlots();
    }

    public override void UIExit()
    {
        Debug.Log("Exiting InventoryOff state");
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