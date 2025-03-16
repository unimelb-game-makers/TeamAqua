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
            {
                dialoguePopup.HidePopup();
                StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
                return;
            }
            if (!pausePopup.isShowing && !pausePopup.isAnimating )
            {
                if(journalPopup.isShowing)
                {
                    journalPopup.HidePopup();
                }
                pausePopup.ShowPopup();
            }
            else if (pausePopup.isShowing && !pausePopup.isAnimating)
                pausePopup.HidePopup();
        }

        private void ToggleJournal()
        {
            if (dialoguePopup.isShowing || pausePopup.isShowing)
                return;
            if (!journalPopup.isShowing && !journalPopup.isAnimating)
                journalPopup.ShowPopup();
            else if(journalPopup.isShowing && !journalPopup.isAnimating)
                journalPopup.HidePopup();
        }

        private void OnDialogueStart()
        {
            if (pausePopup.isShowing)
                return;
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

        private void OnDestroy()
        {
            DialogueSystem.OnDialogueStart -= OnDialogueStart;
            DialogueSystem.OnDialogueEnd -= OnDialogueEnd; 
        }
    }
}
