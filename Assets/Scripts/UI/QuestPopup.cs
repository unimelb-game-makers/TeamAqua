using System.Collections.Generic;
using System.Linq;
using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;

namespace UI
{
    public class QuestPopup : Popup
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text questText;
        protected override void InitPopup()
        {
        }

        private List<QuestData> GetData()
        {
            return QuestManager.instance.GetQuests().Where(q => !q.finished).ToList();
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            List<QuestData> quests = GetData();
            if (quests.Count == 0)
            {
                titleText.text = "Quests";
                questText.text = "No Quests,  You're All Done!";
            }
            else
            {
                ShowQuest(quests[0]);
            }
        }

        private void ShowQuest(QuestData quest)
        {
            questText.text = "";
            titleText.text = quest.title;
            // write the quest into the questText
            questText.text = quest.description + "\n" 
                          + "Task: " 
                          + quest.quest_steps[quest.current_step_number].description + "\n"
                          + "Step: " + (quest.current_step_number + 1) + "/" + quest.quest_steps.Count + "\n"
                          + "<color=red>Current Objective:</color>" + "\n"  // Objective in red
                          +  quest.quest_steps[quest.current_step_number].objective + "\n";
                    
            if (quest.quest_steps[quest.current_step_number].quest_item_id != -1)
            {
                // TODO: replace the item ID with the actual item name after implementing the inventory system and items etc
                questText.text += "Item: " + quest.quest_steps[quest.current_step_number].quest_item_id + "\n";
                questText.text += "Amount: " + quest.quest_steps[quest.current_step_number].quest_item_amount + "\n";
            }

            questText.text += "\n"
                              + "<color=green>Reward: " + quest.reward.exp + " exp " + quest.reward.gold +
                              " gold</color>";
        }
    }
}
