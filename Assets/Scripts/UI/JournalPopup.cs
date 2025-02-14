using System;
using Kuroneko.UIDelivery;
using UnityEngine;
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
    
    
        [SerializeField] private Button inventoryButton;
        [SerializeField] private Button questButtion;
        [SerializeField] private Button mapButton;
    
        protected override void InitPopup()
        {
            inventoryButton.onClick.AddListener(ShowInventory);
            questButtion.onClick.AddListener(ShowQuest);
            mapButton.onClick.AddListener(ShowMap);
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            ShowTab(currentTab);
        }
    
        private void ShowInventory()
        {
            ShowTab(JournalTab.Quest);
        }

        private void ShowQuest()
        {
            ShowTab(JournalTab.Quest);
        }

        private void ShowMap()
        {
            ShowTab(JournalTab.Quest);
        }

        private void ShowTab(JournalTab tab)
        {
            inventoryPopup.SetActive(tab == JournalTab.Inventory);
            questPopup.SetActive(tab == JournalTab.Quest);
            mapPopup.SetActive(tab == JournalTab.Map);
        }

    }
}