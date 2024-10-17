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
        inventoryMenu.SetActive(false);
        UpdateSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !_menuActivated && !DialogueSystem.GetIsPlaying())
        {
            UpdateSlots();
            inventoryMenu.SetActive(true);
            _menuActivated = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && _menuActivated)
        {
            inventoryMenu.SetActive(false);
            _menuActivated = false;
        }
        if (DialogueSystem.GetIsPlaying()) // forcibly closes inventory if player enters dialogue
        {
            inventoryMenu.SetActive(false);
            _menuActivated = false;
        }
    }

    private void OnEnable()
    {
        //menuAction.Enable();
    }

    private void OnDisable()
    {
        //menuAction.Disable();
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