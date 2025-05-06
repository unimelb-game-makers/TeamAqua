using System.Collections.Generic;
using System.Linq;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class QuestPopup : Popup
    {
        [SerializeField] private ItemDatabase itemDatabase;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text questText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button cancelButton;

        private List<QuestData> questData = new();
        private int index = 0;
        protected override void InitPopup()
        {
            Debug.Log("QuestPopup InitPopup");
            nextButton.onClick.AddListener(NextQuest);
            previousButton.onClick.AddListener(PreviousQuest);
            cancelButton.onClick.AddListener(CancelQuest);
            
            nextButton.gameObject.SetActiveFast(false);
            previousButton.gameObject.SetActiveFast(false);
            cancelButton.gameObject.SetActiveFast(false);
        }

        private List<QuestData> GetData()
        {
            return QuestManager.instance.GetQuests().Where(q => !q.finished).ToList();
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            questData = GetData();
            if (questData.Count == 0)
            {
                titleText.text = "Quests";
                questText.text = "No Quests,  You're All Done!";
            }
            else
            {
                ShowQuest(0);
            }
        }

        private void ShowQuest(int nextIndex)
        {
            index = nextIndex; 
            // Activate the buttons
            nextButton.gameObject.SetActiveFast(index < questData.Count - 1);
            previousButton.gameObject.SetActiveFast(index > 0);
            
            QuestData quest = questData[index];
            questText.text = "";
            titleText.text = quest.title;
            // write the quest into the questText
            questText.text = quest.description + "\n" 
                          + "Task: " 
                          + quest.quest_steps[quest.current_step_number].description + "\n"
                          + "Step: " + (quest.current_step_number + 1) + "/" + quest.quest_steps.Count + "\n"
                          + "<color=red>Current Objective:</color>" + "\n"  // Objective in red
                          +  quest.quest_steps[quest.current_step_number].objective + "\n";
                    
            if (quest.quest_steps[quest.current_step_number].RequiresItem() && itemDatabase.TryGetItem(quest.quest_steps[quest.current_step_number].quest_item_id, out Item item))
            {
                questText.text += "Item: " + item.displayName + "\n";
                questText.text += "Amount: " + quest.quest_steps[quest.current_step_number].quest_item_amount + "\n";
            }

            questText.text += "\n"
                              + "<color=green>Reward: " + quest.reward.exp + " exp " + quest.reward.gold +
                              " gold</color>";
        }

        private void NextQuest()
        {
            ShowQuest(index + 1);
        }

        private void PreviousQuest()
        {
            ShowQuest(index - 1);
        }

        private void CancelQuest()
        {
            
        }
    }
}
