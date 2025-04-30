using Kuroneko.UIDelivery;
using UnityEngine;

namespace Popups
{
    public class UIController : Popup
    {
        [SerializeField] private HUD hud;
        [SerializeField] private JournalPopup journalPopup;
        [SerializeField] public PausePopup pausePopup;
        [SerializeField] private DialoguePopup dialoguePopup;
        [SerializeField] private FadePopup fadePopup;

        //TODO(Alex): PLEASE MOVE THIS OUT OF UI
        public static bool Paused = false;

        protected override void InitPopup()
        {
            DialogueManager.OnDialogueStart += OnDialogueStart;
            DialogueManager.OnDialogueEnd += OnDialogueEnd;
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

        private bool CanPause()
        {
            return !pausePopup.isShowing && !pausePopup.isAnimating;
        }

        private bool CanUnpause()
        {
            return pausePopup.isShowing && !pausePopup.isAnimating;
        }

        private void TogglePause()
        {
            if (dialoguePopup.isShowing)
            {
                dialoguePopup.HidePopup();
                StartCoroutine(DialogueManager.Instance().ExitDialogueMode());
                return;
            }

            if (CanPause() && !Paused)
            {
                if(journalPopup.isShowing)
                {
                    journalPopup.HidePopup();
                }
                pausePopup.ShowPopup();
                Paused = true;
            }
            else if (CanUnpause() && Paused)
            {
                pausePopup.HidePopup();
                Paused = false;
            }
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
                // IN PROCESS: HOW TO PREVENT ENTERDIALOGUEMODE IF PAUSE IS SHOWING
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
            DialogueManager.OnDialogueStart -= OnDialogueStart;
            DialogueManager.OnDialogueEnd -= OnDialogueEnd; 
        }
    }
}
