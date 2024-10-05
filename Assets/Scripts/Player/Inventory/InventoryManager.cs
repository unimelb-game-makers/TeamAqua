using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;    

// INPUT ACTION TO BE REWORKED
/*
public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    //private bool _menuActivated;
    //public InputAction menuAction;
    public ItemSlot[] itemSlot; 
    void Start()
    {

    }

    void Update()
    {
        if (menuAction.triggered && !_menuActivated)
        {
            inventoryMenu.SetActive(true);
            _menuActivated = true;
        }
        
        else if (menuAction.triggered && _menuActivated)
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

    public void AddItem(string item, int quantity, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item, quantity, itemSprite);
                return;
            }
                
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
*/