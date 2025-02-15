using Kuroneko.UIDelivery;
using UnityEngine;

namespace UI
{
    public class UIController : Popup
    {
        [SerializeField] private HUD hud;
        [SerializeField] private JournalPopup journalPopup;
        [SerializeField] private PausePopup pausePopup;
        [SerializeField] private DialoguePopup dialoguePopup;

        protected override void InitPopup()
        {
            DialogueSystem.OnDialogueStart += OnDialogueStart;
            DialogueSystem.OnDialogueEnd += OnDialogueEnd;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleJournal();
            }
        }

        private void TogglePause()
        {
            if (dialoguePopup.isShowing)
                return;
            if (!pausePopup.isShowing && !pausePopup.isAnimating)
                pausePopup.ShowPopup();
            else if (pausePopup.isShowing && !pausePopup.isAnimating)
                pausePopup.HidePopup();
        }

        private void ToggleJournal()
        {
            if (dialoguePopup.isShowing)
                return;
            if (!journalPopup.isShowing && !journalPopup.isAnimating)
                journalPopup.ShowPopup();
            else if(journalPopup.isShowing && !journalPopup.isAnimating)
                journalPopup.HidePopup();
        }

        private void OnDialogueStart()
        {
            hud.HidePopup();
            if(journalPopup.isShowing)
                journalPopup.HidePopup();
            dialoguePopup.ShowPopup();
        }

        private void OnDialogueEnd()
        {
            hud.ShowPopup();
            dialoguePopup.HidePopup();
        }
    }
}
