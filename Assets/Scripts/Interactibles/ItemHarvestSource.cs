using System;
using UnityEngine;
using UnityEngine.Serialization;

/*
Make sure to set the gameobject tag to "ItemResource".
*/

public class ItemHarvestSource : MonoBehaviour
{
    [SerializeField] private HarvestData[] harvestList = Array.Empty<HarvestData>();
    private int itemIDX = 0;
    private Vector3 orig_scale; //save value of the original scale for tweening
    float popFactor = 0.8f;

    [SerializeField] private InteractablePopup interactablePopup;

    private void Start()
    {
        orig_scale = transform.localScale;
        if (interactablePopup == null)
        {
            Debug.LogError($"{name} is missing an InteractablePopup");
        }
        else
        {
            interactablePopup.HidePopup();
        }
    }

    public void OnPlayerEnter()
    {
        if(interactablePopup)
            interactablePopup.ShowPopup();
    }

    public void OnPlayerExit()
    {
        if(interactablePopup)
            interactablePopup.HidePopup();
    }

    public bool HarvestResource(out Item item)
    {
        // If we are past the list, then return
        if (!CanHarvest())
        {
            item = null;
            return false;
        }

        HarvestData currentItem = harvestList[itemIDX];
        currentItem.amount -= 1;
        Pop();
        if (currentItem.amount == 0)
        {
            if (itemIDX + 1 >= harvestList.Length)
                gameObject.SetActive(false);
            else
                itemIDX++;
        }
        if (EnergyManager.instance != null)
            EnergyManager.instance.LoseEnergy(currentItem.energyCost);

        AudioManager.Instance.Play(currentItem.AudioName);
        item = currentItem.itemResource;
        return true;
    }

    private bool CanHarvest()
    {
        if (itemIDX >= harvestList.Length)
            return false;
        return harvestList[itemIDX].amount > 0 && EnergyManager.instance.HasEnergy(harvestList[itemIDX].energyCost);
    }

    private void Pop()
    {
        LeanTween
            .scale(gameObject, orig_scale * popFactor, 0.1f)
            .setDelay(.1f)
            .setEaseInOutBounce();
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
