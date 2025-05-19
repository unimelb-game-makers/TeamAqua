using Kuroneko.UIDelivery;
using UnityEngine;

namespace Popups
{
    public class MainMenuPopup : Popup
    {
        [SerializeField] private StartPopup startPopup;
        [SerializeField] private SavePopup savePopup;
        
        protected override void InitPopup()
        {
            startPopup.Init(this);
            savePopup.Init(this);
            ShowStartPopup();
        }

        public void ShowSavePopup()
        {
            startPopup.HidePopup();
            savePopup.ShowPopup();
        }

        public void ShowStartPopup()
        {
            startPopup.ShowPopup();
            savePopup.HidePopup();
        }
    }
}
