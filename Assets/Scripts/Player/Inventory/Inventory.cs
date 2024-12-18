using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private void Awake()
    {
        inventory = this;
    }

    private void Start()
    {
        //AddItem(test_item);
        //SubtractItem(test_item, 3);
    }

    /*Get the inventory item data of an item*/
    public InventoryItem GetItemData(Item item)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            /*Found item*/
            if (inventoryItem.item == item)
            {
                return inventoryItem;
            }
        }
        /*Item not found*/
        return null;
    }

    public void AddItem(Item item)
    {
        /*Go through inventory list and see if item already in*/
        InventoryItem inventoryItem = GetItemData(item);
        if (inventoryItem != null)
        {
            inventoryItem.count++;
        }
        /*Else, add to list*/
        else
        {
            inventoryItem = new InventoryItem(item);
            inventoryItems.Add(inventoryItem);
        }
    }

    /*Returns 1 if successfully subtracted item. Else 0*/
    public int SubtractItem(Item item, int amount)
    {
        InventoryItem inventoryItem = GetItemData(item);
        if (inventoryItem != null)
        {
            if (amount <= inventoryItem.count)
            {
                inventoryItem.count -= amount;
                return 1;
            }
            else return 0;
        }
        else return 0;
    }

    public bool HasItem(int item_id, int amount)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.item.itemID == item_id && inventoryItem.count >= amount)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(int item_id, int amount)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.item.itemID == item_id)
            {
                inventoryItem.count -= amount;
                if (inventoryItem.count <= 0)
                {
                    inventoryItems.Remove(inventoryItem);
                }
                return;
            }
        }
    }
}
/*
This is the type that the inventory list will use 
to keep track of each item and its count.
*/
[Serializable]
public class InventoryItem
{
    public Item item;
    public int count;

    /*Constructor used for adding new items to inventory list.*/
    public InventoryItem(Item newItem)
    {
        item = newItem;
        count = 1;
    }
}