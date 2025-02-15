using System.Collections.Generic;
using Ink.Runtime;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using UnityEngine;

namespace UI
{
    public class DialogueChoicePopup : Popup
    {
        [SerializeField] private RectTransform popupHolder;
        [SerializeField] private DialogueChoicePopupItem samplePopupItem;

        private List<Choice> data = new();
        private List<DialogueChoicePopupItem> popupItems = new();
        
        protected override void InitPopup()
        {
        }

        public void Init(List<Choice> choices)
        {
            data = choices;
        }

        public override void ShowPopup()
        {
            base.ShowPopup();

            int totalToSpawn = data.Count - popupItems.Count;

            if (totalToSpawn > 0)
            {
                samplePopupItem.gameObject.SetActiveFast(true);
                for (int i = 0; i < totalToSpawn; i++)
                {
                    DialogueChoicePopupItem item = Instantiate(samplePopupItem, popupHolder);
                    popupItems.Add(item);
                }
            }
            
            for (int i = 0; i < popupItems.Count; i++)
                popupItems[i].gameObject.SetActiveFast(false);

            for (int i = 0; i < data.Count; i++)
            {
                if (i < popupItems.Count)
                {
                    popupItems[i].Init(data[i]);
                    popupItems[i].gameObject.SetActiveFast(true);
                }
            }
        }
    }
}
