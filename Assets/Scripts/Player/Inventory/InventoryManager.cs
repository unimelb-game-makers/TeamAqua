using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem;    

// INPUT ACTION TO BE REWORKED

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject scrollParent;
    [SerializeField] GameObject inventoryMenu;
    private bool _menuActivated;
    //public InputAction menuAction;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    
    void Start()
    {
        //inventoryMenu.SetActive(false);
        OnDisableI();
        UpdateSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && UIinputProvider.instance().UI_canOpen[1] && !_menuActivated && !DialogueSystem.GetIsPlaying())
        {
            OnEnableI();
        }
        else if (Input.GetKeyDown(KeyCode.I) && _menuActivated)
        {
            OnDisableI();
        }
        if (DialogueSystem.GetIsPlaying() || PausePanelScript.instance().isPaused) // forcibly closes inventory if player enters dialogue
        {
            OnDisableI();
        }
        UpdateSlots();
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

    public void UpdateSlots()
    {
        bool found;
        //Get every element from inventory, and put it in a slot
        foreach(var inventoryItem in inventory.inventory)
        {
            found = false;
            //Debug.Log($"Cycled {inventoryItem.item.itemName}");
            foreach(var itemSlot in itemSlots)
            {
                if(itemSlot.item_data.item == inventoryItem.item)
                {
                    itemSlot.SetItem(inventoryItem);
                    found = true;
                    //Debug.Log($"Updated {inventoryItem.item.itemName}");
                }
            }
            if(found == false)
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
        foreach(var itemSlot in itemSlots)
        {
            itemSlot.selectedShader.SetActive(false);
            itemSlot.thisItemSelected = false;
        }
    }
}