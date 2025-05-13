using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private List<QuestTracker> _questData = new();
        private int _index = 0;
        
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

        private List<QuestTracker> GetData()
        {
            return QuestManager.instance.Quests.Where(q => q.state != QuestState.Submitted).ToList();
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            _questData = GetData();
            if (_questData.Count == 0)
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
            _index = nextIndex; 
            // Activate the buttons
            nextButton.gameObject.SetActiveFast(_index < _questData.Count - 1);
            previousButton.gameObject.SetActiveFast(_index > 0);
            
            Quest quest = _questData[_index].quest;
            StringBuilder questBody = new();
            
            titleText.SetText(quest.title);
            questBody.AppendLine(quest.description);
            for (int i = 0; i < quest.steps.Count; ++i)
            {
                questBody.AppendLine($"Task: {quest.steps[i].description}");
                if (quest.steps[i].type == QuestType.Gather)
                {
                    List<QuestItem> items = quest.steps[i].requiredItems;
                    for (int j = 0; j < items.Count; ++j)
                    {
                        if (itemDatabase.TryGetItem(items[j].item.name, out Item item))
                        {
                            questBody.AppendLine($"Item: {item.displayName}");
                            questBody.AppendLine($"Amount: {items[j].amount}");
                        }
                    }
                }
            }

            questBody.AppendLine();
            questBody.AppendLine("<color=green>Reward: " + quest.reward.exp + " exp " + quest.reward.gold + " gold</color>");
            questText.SetText(questBody.ToString());
        }

        private void NextQuest()
        {
            ShowQuest(_index + 1);
        }

        private void PreviousQuest()
        {
            ShowQuest(_index - 1);
        }

        private void CancelQuest()
        {
            
        }
    }
}
