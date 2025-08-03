using System;
using UnityEngine;

public class ItemHarvester : MonoBehaviour
{
    [NonSerialized]
    public ItemHarvestSource source = null;
    private ItemHarvestSource oldSource = null;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && source != null && (DialogueManager.Instance().State == DialogueState.None))
        {
            if (source.HarvestResource(out Item item))
            {
                InventoryManager.instance.AddItem(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource"))
        {
            if(source != null) oldSource = source; // If there already was a source, remember which one it was
            source = other.gameObject.GetComponent<ItemHarvestSource>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource"))
        {
            if (source == oldSource) // Exited the source with no new source entered
                source = null;
            else                     // Exited the source but there is a new source
                oldSource = source;
        }
    }
}