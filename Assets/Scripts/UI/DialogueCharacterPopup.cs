using System.Collections.Generic;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using UnityEngine;

namespace Popups
{
    public class DialogueCharacterPopup : Popup
    {
        // INK TAGS
        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string AUDIO_TAG = "audio";
        private const string CUTSCENE_TAG = "cutscene";

        [SerializeField]
        private DialogueCharacterPopupItem leftCharacter;

        [SerializeField]
        private DialogueCharacterPopupItem rightCharacter;

        [SerializeField]
        private Animator cutscene;

        protected override void InitPopup()
        {
            cutscene.gameObject.SetActiveFast(false);
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            leftCharacter.gameObject.SetActiveFast(false);
            rightCharacter.gameObject.SetActiveFast(false);
        }

        public void HandleTags(List<string> tags)
        {
            foreach (string inkTag in tags)
            {
                // parse the tag
                string[] splitTag = inkTag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError("error: tag could not be parsed: " + inkTag);
                }
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                // handle the tags aside from quests
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        SetSpeaker(tagValue);
                        break;
                    case PORTRAIT_TAG:
                        SetPortrait(tagValue);
                        break;
                    case AUDIO_TAG:
                        SetAudio(tagValue);
                        break;
                    case CUTSCENE_TAG:
                        SetCutscene(tagValue);
                        break;
                    default:
                        Debug.LogWarning("tag came in but is not being handled: " + inkTag);
                        break;
                }
            }
        }

        public void SetSpeaker(string tagValue)
        {
            // Noelle only apperas on the left
            // Other characters only appear on the right
            leftCharacter.gameObject.SetActiveFast(tagValue.Contains("Noelle"));
            leftCharacter.SetName(tagValue);
            rightCharacter.gameObject.SetActiveFast(tagValue != "Noelle" && tagValue != "Narrator");
            rightCharacter.SetName(tagValue);

            DialogueManager
                .Instance()
                .DialogueAudioPlayer.SetCurrentAudioInfo(
                    tagValue == "Narrator" ? "Narrator" : tagValue
                );
        }

        public void SetAudio(string tagValue)
        {
            DialogueManager.Instance().DialogueAudioPlayer.SetCurrentAudioInfo(tagValue);
        }

        public void SetCutscene(string tagValue)
        {
            cutscene.gameObject.SetActiveFast(true);
            cutscene.Play(tagValue);
            Debug.Log("cutscene frame is " + tagValue);
        }

        public void SetPortrait(string tagValue)
        {
            if (tagValue.Contains("Noelle"))
            {
                //leftCharacter.gameObject.SetActiveFast(true);
                leftCharacter.PlayAnim(tagValue);
            }
            else
                //rightCharacter.gameObject.SetActiveFast(true);
                rightCharacter.PlayAnim(tagValue);
        }
    }
}
