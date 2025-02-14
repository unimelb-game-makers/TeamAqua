using System;
using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum JournalTab
{
    INVENTORY,
    QUEST,
    MAP,
}

public class JournalPopup : Popup
{
    [SerializeField] private JournalTab currentTab = JournalTab.INVENTORY;
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
        ShowTab(JournalTab.QUEST);
    }

    private void ShowQuest()
    {
        ShowTab(JournalTab.QUEST);
    }

    private void ShowMap()
    {
        ShowTab(JournalTab.QUEST);
    }

    private void ShowTab(JournalTab tab)
    {
        inventoryPopup.SetActive(tab == JournalTab.INVENTORY);
        questPopup.SetActive(tab == JournalTab.QUEST);
        mapPopup.SetActive(tab == JournalTab.MAP);
    }

}
