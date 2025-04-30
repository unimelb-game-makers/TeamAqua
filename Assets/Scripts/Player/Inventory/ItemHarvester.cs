using System;
using UnityEngine;

public class ItemHarvester : MonoBehaviour
{
    [NonSerialized]
    public ItemHarvestSource source = null;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && source != null && source.currentItem().amount > 0)
        {
            Inventory.inventory.AddItem(source.HarvestResource());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource"))
        {
            source = other.gameObject.GetComponent<ItemHarvestSource>();
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
