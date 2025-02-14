using Kuroneko.UIDelivery;
using UnityEngine;

namespace UI
{
    public class UIController : Popup
    {
        [SerializeField] private JournalPopup journalPopup;
        [SerializeField] private PausePopup pausePopup;

        protected override void InitPopup()
        {
        }
    }
}
