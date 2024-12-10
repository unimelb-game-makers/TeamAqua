using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Make sure to set the gameobject tag to "ItemResource".
*/

public class ItemHarvestSource : MonoBehaviour
{
    [SerializeField] Item itemResource;
    [SerializeField] public int amountClicks = 1;
    [SerializeField] int energyCost;
    [SerializeField] float popFactor = 0.8f;
    private EnergyManager EnergyMana;

    private Vector3 orig_scale; //save value of the original scale for tweening

    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        orig_scale = transform.localScale;
    }

    /*Maybe change it so that player can harvest multiples of item*/
    public Item HarvestResource()
    {   //it seems the bug bill showed on 28th nov was because we have nothing to stop amountClicks from going below 0, 
        // so ive put a an if statement here to check, however, returning null in else is blasting errors like crazy (every frame after item harvested) so i (or someone else) will need to find an alternative in that else statement.
        
        amountClicks -= 1;
        pop();
        if(amountClicks == 0) gameObject.SetActive(false);
        EnergyMana.LoseEnergy(energyCost);
        Debug.Log($"Harvested Item: {itemResource.name}");
        return itemResource;
    }
    void pop()
    {
        LeanTween.scale(gameObject, orig_scale * popFactor, 0.1f).setDelay(.1f).setEaseInOutBounce();
        LeanTween.scale(gameObject, orig_scale, 0.1f).setDelay(.2f).setEaseInOutBounce();
    }
}
