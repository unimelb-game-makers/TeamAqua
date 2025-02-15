using Kuroneko.UIDelivery;
using UnityEngine;

namespace UI
{
    public class DialogueChoicePopup : Popup
    {
        [SerializeField] private RectTransform popupHolder;
        [SerializeField] private DialogueChoicePopupItem samplePopupItem;
        
        protected override void InitPopup()
        {
        }
    }
}
