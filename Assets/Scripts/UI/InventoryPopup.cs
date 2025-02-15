using System;
using System.Collections.Generic;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class InventoryPopup : Popup
    {
        [SerializeField] private RectTransform popupHolder;
        [SerializeField] private InventoryPopupItem samplePopupItem;
        [SerializeField] private RectTransform emptyDescription;

        [NonSerialized, ShowInInspector, ReadOnly]
        private List<InventoryPopupItem> items = new();
        protected override void InitPopup()
        {
            
        }

        private List<InventoryItem> GetData()
        {
            return Inventory.inventory.inventoryItems;
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            List<InventoryItem> data = GetData();
            int itemsToSpawn = data.Count - items.Count;
            if (itemsToSpawn > 0)
            {
                samplePopupItem.gameObject.SetActiveFast(true);
                for (int i = 0; i < itemsToSpawn; i++)
                {
                    InventoryPopupItem item = Instantiate(samplePopupItem, popupHolder);
                    items.Add(item);
                }
            }

            samplePopupItem.gameObject.SetActiveFast(false);

            for (int i = 0; i < items.Count; i++)
                items[i].gameObject.SetActiveFast(false);
            for (int i = 0; i < data.Count; i++)
            {
                items[i].Init(data[i]);
                items[i].gameObject.SetActiveFast(true);
            }

            emptyDescription.gameObject.SetActiveFast(data.Count == 0);
            popupHolder.gameObject.SetActiveFast(data.Count > 0);
        }
    }
}
