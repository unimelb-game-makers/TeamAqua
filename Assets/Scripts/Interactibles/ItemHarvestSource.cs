using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Make sure to set the gameobject tag to "ItemResource".
*/

public class ItemHarvestSource : MonoBehaviour
{
    [SerializeField] HarvestData[] harvestList;
    private int itemIDX = 0;
    private EnergyManager EnergyMana;
    private Vector3 orig_scale; //save value of the original scale for tweening
    float popFactor = 0.8f;
    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        orig_scale = transform.localScale;
    }

    public Item HarvestResource()
    {   //it seems the bug bill showed on 28th nov was because we have nothing to stop amountClicks from going below 0, 
        // so ive put a an if statement here to check, however, returning null in else is blasting errors like crazy (every frame after item harvested) so i (or someone else) will need to find an alternative in that else statement.
        if (itemIDX < harvestList.Length)
        {
            currentItem().amount -= 1;
            pop();
            if (currentItem().amount == 0)
            {
                if (itemIDX + 1 >= harvestList.Length)
                    gameObject.SetActive(false);
                else
                    itemIDX++;
            }
            if (EnergyMana != null)
                EnergyMana.LoseEnergy(currentItem().energyCost);
            
            AudioManager.Instance.Play(currentItem().AudioName);
            Debug.Log($"Harvested Item: {currentItem().itemResource.name}");
            return currentItem().itemResource;
        }
        else
        {
            return null;
        }
    }
    public HarvestData currentItem()
    {
        return harvestList[itemIDX];
    }
    void pop()
    {
        LeanTween.scale(gameObject, orig_scale * popFactor, 0.1f).setDelay(.1f).setEaseInOutBounce();
        LeanTween.scale(gameObject, orig_scale, 0.1f).setDelay(.2f).setEaseInOutBounce();
    }
}
[Serializable]
public class HarvestData
{
    public Item itemResource;
    public int amount = 1;
    public int energyCost;

    public string AudioName;

}