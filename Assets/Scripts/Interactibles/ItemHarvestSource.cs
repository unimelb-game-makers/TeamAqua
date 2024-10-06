using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Make sure to set the gameobject tag to "ItemResource".
*/

public class ItemHarvestSource : MonoBehaviour
{
    [SerializeField] Item itemResource;
    [SerializeField] int amountClicks = 1;

    /*Maybe change it so that player can harvest multiples of item*/
    public Item HarvestResource()
    {
        amountClicks -= 1;
        if(amountClicks == 0) gameObject.SetActive(false);
        Debug.Log($"Harvested Item: {itemResource.name}");
        return itemResource;
    }
}
