using System;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using NUnit.Framework;
using UnityEngine;

namespace Popups
{
    [Serializable]
    public class QuestIndicator
    {
        public QuestState state;
        public RectTransform indicator;
    }
    public class IndicatorPopup : Popup
    {
        [SerializeField] private RectTransform dialogueIndicator;
        [SerializeField] private QuestIndicator[] questIndicators = Array.Empty<QuestIndicator>();
        
        protected override void InitPopup()
        {
            HidePopup();
        }

        public void ShowDialogue()
        {
            ShowPopup();
            dialogueIndicator.gameObject.SetActiveFast(true);
            for (int i = 0; i < questIndicators.Length; ++i)
            {
                questIndicators[i].indicator.gameObject.SetActiveFast(false);
            }
        }

        public void ShowQuest(QuestState state)
        {
            ShowPopup();
            dialogueIndicator.gameObject.SetActiveFast(false);
            for (int i = 0; i < questIndicators.Length; ++i)
            {
                questIndicators[i].indicator.gameObject.SetActiveFast(questIndicators[i].state == state);
            }
        }
    }
}