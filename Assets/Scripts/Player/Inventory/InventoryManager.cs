using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem;    

// INPUT ACTION TO BE REWORKED

public class InventoryManager : UIState
{
    public static InventoryManager invMana;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject scrollParent;
    [SerializeField] public UIState All_UI_Off;
    public UIState paused;
    [SerializeField] GameObject inventoryMenu;
    private bool _menuActivated;
    //public InputAction menuAction;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    void Awake()
    {
        invMana = this;
    }

    public static InventoryManager instance()
    {
        return invMana;
    }

    public override void UIEnter()
    {
        //inventoryMenu.SetActive(false);
        Debug.Log("entering inventory on state");
        //menuAction.Enable();
        InventoryManager.instance().UpdateSlots();
        inventoryMenu.SetActive(true);
        UpdateSlots();
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

        UpdateSlots();
    }

    public void UpdateSlots()
    {
        bool found;
        //Get every element from inventory, and put it in a slot
        foreach (var inventoryItem in Inventory.inventory.inventoryItems)
        {
            found = false;
            //Debug.Log($"Cycled {inventoryItem.item.itemName}");
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.item_data.item == inventoryItem.item)
                {
                    itemSlot.SetItem(inventoryItem);
                    found = true;
                    //Debug.Log($"Updated {inventoryItem.item.itemName}");
                }
            }
            if (found == false)
            {
                //Debug.Log($"Instanced {inventoryItem.item.itemName}");
                GameObject newItemSlot = Instantiate(itemSlotPrefab, scrollParent.transform);
                newItemSlot.transform.SetParent(scrollParent.transform);
                newItemSlot.GetComponent<ItemSlot>().SetItem(inventoryItem);
                itemSlots.Add(newItemSlot.GetComponent<ItemSlot>());
            }
        }
    }

    public void DeselectAllSlots()
    {
        foreach (var itemSlot in itemSlots)
        {
            itemSlot.selectedShader.SetActive(false);
            itemSlot.thisItemSelected = false;
        }
    }


    public override void UIExit()
    {
        Debug.Log("Exiting InventoryOn state");
    }
}