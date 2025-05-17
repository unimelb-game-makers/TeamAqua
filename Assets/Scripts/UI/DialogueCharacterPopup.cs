using System.Collections.Generic;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using UnityEngine;

namespace Popups
{
    public class DialogueCharacterPopup : Popup
    {
        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";
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
                    /*  --------------- might go unused, unsure if ID checking necessary or not
                    case ID_TAG:        //check for ID at the top of each ink file and compare it to ID of quest
                     id = int.Parse(tagValue); --> try to convert tagvalue to int
                    break;
                    */
                    case SPEAKER_TAG: //change speaker name depending on the speaker tag
                        // Noelle only apperas on the left
                        // Other characters only appear on the right
                        leftCharacter.gameObject.SetActiveFast(tagValue == "Noelle");
                        leftCharacter.SetName(tagValue);
                        rightCharacter.gameObject.SetActiveFast(
                            tagValue != "Noelle" && tagValue != "Narrator"
                        );
                        rightCharacter.SetName(tagValue);
                        DialogueManager
                            .Instance()
                            .DialogueAudioPlayer.SetCurrentAudioInfo(
                                tagValue == "Narrator" ? "Narrator" : tagValue
                            );
                        break;

                    case PORTRAIT_TAG: //change speaker portrait depending on portrait tag
                        if (tagValue.Contains("Noelle"))
                        {
                            //leftCharacter.gameObject.SetActiveFast(true);
                            leftCharacter.PlayAnim(tagValue);
                        }
                        else
                            //rightCharacter.gameObject.SetActiveFast(true);
                            rightCharacter.PlayAnim(tagValue);
                        break;
                    case AUDIO_TAG:
                        DialogueManager
                            .Instance()
                            .DialogueAudioPlayer.SetCurrentAudioInfo(tagValue);
                        break;
                    default:
                        Debug.LogWarning(
                            "tag came in but is not currently being handled: " + inkTag
                        );
                        break;
                    case CUTSCENE_TAG: //change speaker portrait depending on portrait tag
                        cutscene.gameObject.SetActiveFast(true);
                        cutscene.Play(tagValue);
                        Debug.Log("cutscene frame is " + tagValue);
                        break;
                }
            }
        }
    }
}
