using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Inventory : MonoBehaviour
// {
    
//     public List<InventoryItem> inventory = new List<InventoryItem>();

//     public void AddItem(Item item)
//     {
//         /*Go through inventory list and see if item already in*/
//         bool found = false;
//         foreach(var inventoryItem in inventory)
//         {
//             /*If item already exists, increment 1*/
//             if(inventoryItem.item == item)
//             {
//                 inventoryItem.count++;
//                 found = true;
//             }
//         }
//         /*Else, add to list*/
//         if(found == false)
//         {
//             InventoryItem inventoryItem = new InventoryItem(item);
//             inventory.Add(inventoryItem);
//         }
//     }
// }
// /*
// This is the type that the inventory list will use 
// to keep track of each item and its count.
// */
// [Serializable]
// public class InventoryItem
// {
//     public Item item;
//     public int count;

//     /*Constructor used for adding new items to inventory list.*/
//     public InventoryItem(Item newItem)
//     {
//         item = newItem;
//         count = 1;
//     }
// }