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
    [SerializeField] int energyCost;
    private EnergyManager EnergyMana;

    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
    }

    /*Maybe change it so that player can harvest multiples of item*/
    public Item HarvestResource()
    {
        amountClicks -= 1;
        pop();
        if(amountClicks == 0) gameObject.SetActive(false);
        EnergyMana.LoseEnergy(energyCost);
        Debug.Log($"Harvested Item: {itemResource.name}");
        return itemResource;
    }
    void pop()
    {
        //Debug.Log("LeanTween Scale");
        LeanTween.scale(gameObject, new Vector3(.8f,.8f,.8f), 0.1f).setDelay(.1f).setEaseInOutBounce();
        LeanTween.scale(gameObject, new Vector3(1f,1f,1f), 0.1f).setDelay(.2f).setEaseInOutBounce();
    }
}
