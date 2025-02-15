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
        [SerializeField] private DialogueCharacterPopup characterPopup;
        [SerializeField] private DialogueChoicePopup choicePopup;
        [SerializeField] private Image fastForward;

        private Coroutine lineCoroutine = null;
        private string currentLine;
        private List<Choice> currentChoices = new List<Choice>();
        
        protected override void InitPopup()
        {
            DialogueSystem.OnDialogueContinue += OnDialogueContinue;
            DialogueSystem.OnDialogueTags += OnDialogueTags;
        }

        private void Update()
        {
            if (isShowing && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                PressNext();
            }
        }

        private void PressNext()
        {
            // Skip to the end of the line if possible
            if (lineCoroutine != null)
            {
                StopCoroutine(lineCoroutine);
                EndCoroutine();
            }
            // Else, continue if there are no choices
            else if (currentChoices.Count == 0)
            {
                DialogueSystem.Instance().ContinueStory();
            }
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            choicePopup.HidePopup();
        }

        private void OnDialogueContinue(string story, List<Choice> choices)
        {
            lineCoroutine = StartCoroutine(DisplayLine(story, choices));
        }

        private void OnDialogueTags(List<string> tags)
        {
            characterPopup.HandleTags(tags);
        }
        
        private IEnumerator DisplayLine(string line, List<Choice> choices)
        {
            currentChoices = choices;
            currentLine = line;
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
            
            EndCoroutine();
        }

        private void EndCoroutine()
        {
            dialogueText.maxVisibleCharacters = currentLine.Length;
            if (currentChoices.Count > 0)
            {
                choicePopup.Init(currentChoices);
                choicePopup.ShowPopup();
            }

            lineCoroutine = null; 
        }
        
        private void OnDestroy()
        {
            DialogueSystem.OnDialogueContinue -= OnDialogueContinue;
            DialogueSystem.OnDialogueTags -= OnDialogueTags;
        }
    }
}
