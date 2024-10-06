using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine.EventSystems;
// InventoryManager needs to be tweaked to use ?ListView?

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public InventoryItem item_data;
    
    // ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private void Start()
    {
        SetItem(item_data);
    }
    public void SetItem(InventoryItem invItem)
    {
        item_data = invItem;
        quantityText.text = item_data.count.ToString();
        quantityText.enabled = true;
        itemImage.sprite = item_data.item.sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        // to be reworked
        //_inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }

    public void OnRightClick()
    {
        
    }
}
