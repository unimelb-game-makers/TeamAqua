using System;
using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public enum JournalTab
    {
        Inventory,
        Quest,
        Map,
    }

    public class JournalPopup : Popup
    {
        [SerializeField] private JournalTab currentTab = JournalTab.Inventory;
        [SerializeField] private InventoryPopup inventoryPopup;
        [SerializeField] private QuestPopup questPopup;
        [SerializeField] private MapPopup mapPopup;
    
    
        [SerializeField] private JournalTabButton inventoryButton;
        [SerializeField] private JournalTabButton questButton;
        [SerializeField] private JournalTabButton mapButton;
    
        protected override void InitPopup()
        {
        }

        private void Start()
        {
            inventoryButton.AddListener(ShowInventory);
            questButton.AddListener(ShowQuest);
            mapButton.AddListener(ShowMap);
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            ShowTab(currentTab);
        }
    
        private void ShowInventory()
        {
            ShowTab(JournalTab.Inventory);
        }

        private void ShowQuest()
        {
            ShowTab(JournalTab.Quest);
        }

        private void ShowMap()
        {
            ShowTab(JournalTab.Map);
        }

        private void ShowTab(JournalTab tab)
        {
            currentTab = tab;
            inventoryPopup.SetActive(tab == JournalTab.Inventory);
            questPopup.SetActive(tab == JournalTab.Quest);
            mapPopup.SetActive(tab == JournalTab.Map);
            
            inventoryButton.SetActive(tab == JournalTab.Inventory);
            questButton.SetActive(tab == JournalTab.Quest);
            mapButton.SetActive(tab == JournalTab.Map);
        }

    }
}