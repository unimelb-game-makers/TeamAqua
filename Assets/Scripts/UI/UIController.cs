using Kuroneko.UIDelivery;
using UnityEngine;

namespace UI
{
    public class UIController : Popup
    {
        [SerializeField] private HUD hud;
        [SerializeField] private JournalPopup journalPopup;
        [SerializeField] private PausePopup pausePopup;

        protected override void InitPopup()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                ShowJournal();
            }
        }

        private void TogglePause()
        {
            if (!pausePopup.isShowing && !pausePopup.isAnimating)
                pausePopup.ShowPopup();
            else if (pausePopup.isShowing && !pausePopup.isAnimating)
                pausePopup.HidePopup();
        }

        private void ShowJournal()
        {
            if (!journalPopup.isShowing && !journalPopup.isAnimating)
                journalPopup.ShowPopup();
        }
    }
}
