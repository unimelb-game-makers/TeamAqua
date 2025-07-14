using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class DialoguePopup : Popup
    {
        [Header("Setup")]
        [SerializeField]
        private float typeSpeed = 0.04f;

        [Header("UI Components")]
        [SerializeField]
        private TMP_Text dialogueText;

        [SerializeField]
        private DialogueCharacterPopup characterPopup;

        [SerializeField]
        private DialogueChoicePopup choicePopup;

        [SerializeField]
        private Image fastForward;

        private Coroutine lineCoroutine = null;
        private string currentLine;
        private List<Choice> currentChoices = new List<Choice>();
        string lineOne,
            lineTwo;

        protected override void InitPopup()
        {
            DialogueManager.OnDialogueContinue += OnDialogueContinue;
            DialogueManager.OnDialogueTags += OnDialogueTags;
            DialogueManager.OnDialogueEvents += OnDialogueEvents;
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
                DialogueManager.Instance().ContinueStory();
            }
        }

        [Button]
        public void Skip()
        {
            EndCoroutine();
            DialogueManager.Instance().SkipStory();
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            characterPopup.ShowPopup();
            choicePopup.HidePopup();
        }

        private void OnDialogueContinue(string story, List<Choice> choices, bool skip)
        {
            Debug.Log("lineCoroutine is: " + lineCoroutine);
            lineCoroutine = StartCoroutine(DisplayLine(story, choices, skip));
        }

        private void OnDialogueTags(List<string> tags)
        {
            characterPopup.HandleTags(tags);
        }

        private void OnDialogueEvents(string events)
        {
            characterPopup.HandleEvents(events);
        }

        private IEnumerator DisplayLine(string line, List<Choice> choices, bool skip)
        {
            string[] splits = line.Split(':', 2);
            lineOne = splits[0].Trim();
            lineTwo = splits[1].Trim();
            Debug.Log("line is: " + line);
            Debug.Log("lineOne is: " + lineOne);
            Debug.Log("lineTwo is: " + lineTwo);
            ShowDialogue(lineOne, lineTwo);

            // Then something like
            currentChoices = choices;
            currentLine = lineTwo;

            if (skip)
            {
                dialogueText.SetText(lineTwo);
                EndCoroutine();
                yield break;
            }

            if (fastForward)
                fastForward.gameObject.SetActiveFast(true);
            choicePopup.HidePopup();
            dialogueText.text = lineTwo; //set text to full line, but set visible characters to 0
            dialogueText.maxVisibleCharacters = 0;
            bool isRichText = false;
            foreach (char letter in lineTwo.ToCharArray())
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
                        DialogueManager
                            .Instance()
                            .DialogueAudioPlayer.PlayDialogueSound(
                                dialogueText.maxVisibleCharacters,
                                dialogueText.text[dialogueText.maxVisibleCharacters]
                            );
                    dialogueText.maxVisibleCharacters++;
                    yield return new WaitForSeconds(typeSpeed); // -> use if not freezing time
                }
            }

            EndCoroutine();
        }

        public void ShowDialogue(string speaker, string line)
        {
            ShowPopup();
            characterPopup.SetSpeaker(speaker);
            characterPopup.PlayAudio(speaker);
        }

        private void EndCoroutine()
        {
            dialogueText.maxVisibleCharacters = currentLine.Length;
            if (currentChoices.Count > 0)
            {
                choicePopup.Init(currentChoices);
                choicePopup.ShowPopup();
                if (fastForward)
                    fastForward.gameObject.SetActiveFast(false);
            }

            if (lineCoroutine != null)
            {
                StopCoroutine(lineCoroutine);
                lineCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            DialogueManager.OnDialogueContinue -= OnDialogueContinue;
            DialogueManager.OnDialogueTags -= OnDialogueTags;
            DialogueManager.OnDialogueEvents -= OnDialogueEvents;
        }
    }
}
