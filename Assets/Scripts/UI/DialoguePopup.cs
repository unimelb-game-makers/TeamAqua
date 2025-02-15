using System;
using System.Collections.Generic;
using Ink.Runtime;
using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialoguePopup : Popup
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private DialogueCharacterPopupItem leftCharacter;
        [SerializeField] private DialogueCharacterPopupItem rightCharacter;
        [SerializeField] private DialogueChoicePopup choicePopup;
        [SerializeField] private Image fastForward;
        
        protected override void InitPopup()
        {
            DialogueSystem.OnDialogueStart += OnDialogueStart;
            DialogueSystem.OnDialogueContinue += OnDialogueContinue;
            DialogueSystem.OnDialogueTags += OnDialogueTags;
            DialogueSystem.OnDialogueEnd += OnDialogueEnd;
            DialogueSystem.OnDialogueChoices += OnDialogueChoices;
        }

        private void OnDialogueStart()
        {
            
        }

        private void OnDialogueContinue(string story)
        {
            
        }

        private void OnDialogueTags(List<string> tags)
        {
            
        }

        private void OnDialogueEnd()
        {
            
        }

        private void OnDialogueChoices(List<Choice> choices)
        {
            
        }

        private void OnDestroy()
        {
            DialogueSystem.OnDialogueStart -= OnDialogueStart;
            DialogueSystem.OnDialogueContinue -= OnDialogueContinue;
            DialogueSystem.OnDialogueTags -= OnDialogueTags;
            DialogueSystem.OnDialogueEnd -= OnDialogueEnd;
            DialogueSystem.OnDialogueChoices -= OnDialogueChoices;
        }
    }
}
