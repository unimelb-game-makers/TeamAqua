using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHarvester : MonoBehaviour
{
    [NonSerialized] public ItemHarvestSource source = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && source != null && source.currentItem().amount > 0)
        {
            Debug.Log($"currentItem.amount = {source.currentItem().amount}");
            Inventory.inventory.AddItem(source.HarvestResource());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource"))
        {
            source = other.gameObject.GetComponent<ItemHarvestSource>();
            Debug.Log("Entered a Resource");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource") && source != null)
        {
            source = null;
        }
    }
}
