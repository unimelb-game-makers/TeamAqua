using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class DialoguePopup : Popup
    {
        [Header("Setup")]
        [SerializeField] private float typeSpeed = 0.04f;
        
        [Header("UI Components")]
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
        }

        private void OnDialogueStart()
        {
            ShowPopup();
            choicePopup.HidePopup();
        }

        private void OnDialogueContinue(string story, List<Choice> choices)
        {
            StartCoroutine(DisplayLine(story, choices));
        }

        private void OnDialogueTags(List<string> tags)
        {
            
        }

        private void OnDialogueEnd()
        {
            HidePopup();
        }

        private IEnumerator DisplayLine(string line, List<Choice> choices)
        {
            choicePopup.HidePopup();
            dialogueText.text = line;   //set text to full line, but set visible characters to 0
            dialogueText.maxVisibleCharacters = 0;
            bool isRichText = false;
            foreach (char letter in line.ToCharArray())
            {
                //check for rich text
                if (letter == '<' || isRichText)
                {
                    isRichText = true;
                    if (letter == '>')
                    {
                        isRichText = false;
                    }
                }

                // otherwise, loads letters normally
                else
                {
                    if (dialogueText.maxVisibleCharacters < dialogueText.text.Length)
                        DialogueAudioManager.GetAudioMana().PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                    dialogueText.maxVisibleCharacters++;
                    yield return new WaitForSeconds(typeSpeed);         // -> use if not freezing time
                }
            }

            if (choices.Count > 0)
            {
                choicePopup.Init(choices);
                choicePopup.ShowPopup();
            }
        }

        private void OnDestroy()
        {
            DialogueSystem.OnDialogueStart -= OnDialogueStart;
            DialogueSystem.OnDialogueContinue -= OnDialogueContinue;
            DialogueSystem.OnDialogueTags -= OnDialogueTags;
            DialogueSystem.OnDialogueEnd -= OnDialogueEnd;
        }
    }
}
