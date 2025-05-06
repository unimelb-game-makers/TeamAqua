using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISaveable
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private PlayerSave playerSave;
    public static InventoryManager instance;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Load(SaveSlot saveSlot)
    {
        InventorySaveData saveData = saveSlot.inventorySaveData;
        foreach (ItemSaveData itemData in saveData.items)
        {
            if (itemDatabase.TryGetItem(itemData.id, out Item item))
            {
                AddItem(item, itemData.quantity);
            }
        }
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        ItemSaveData[] items = new ItemSaveData[inventoryItems.Count];
        for (int i = 0; i < inventoryItems.Count; ++i)
        {
            items[i].id = inventoryItems[i].item.name;
            items[i].quantity = inventoryItems[i].count;
        }

        save.inventorySaveData.items = items;
        return save;
    }

    /*Get the inventory item data of an item*/
    private InventoryItem GetItemData(Item item)
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

    public void AddItem(Item item, int amount = 1)
    {
        /*Go through inventory list and see if item already in*/
        InventoryItem inventoryItem = GetItemData(item);
        if (inventoryItem != null)
        {
            inventoryItem.count += amount;
        }
        /*Else, add to list*/
        else
        {
            inventoryItem = new InventoryItem(item, amount);
            inventoryItems.Add(inventoryItem);
        }
    }

    /*Returns true if successfully subtracted item. Else false*/
    public bool SubtractItem(Item item, int amount)
    {
        InventoryItem inventoryItem = GetItemData(item);
        if (inventoryItem != null)
        {
            if (amount <= inventoryItem.count)
            {
                inventoryItem.count -= amount;
                return true;
            }
            return false;
        }
        return false;
    }

    public bool HasItem(string id, int amount)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.item.name == id && inventoryItem.count >= amount)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(string id, int amount)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.item.name == id)
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
    public InventoryItem(Item newItem, int amount = 1)
    {
        item = newItem;
        count = amount;
    }
}
