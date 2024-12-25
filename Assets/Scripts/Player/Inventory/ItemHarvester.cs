using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHarvester : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public ItemHarvestSource source = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && source != null && source.currentItem().amount > 0)
        {
            inventory.AddItem(source.HarvestResource());
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
