using System.Collections.Generic;
using System.Linq;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Popups
{
    public class DialogueCharacterPopup : Popup
    {
        // INK TAGS
        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string AUDIO_TAG = "audio";
        private const string CUTSCENE_TAG = "cutscene";

        // EVENT FUNCTIONS
        private const string EVENT = "EVENT";
        private const string SWAPBGM = "SwapBGM";
        private const string PLAYBGM = "PlayBGM";
        private const string ADDQUEST = "AddQuest";
        private const string SUBMITQUEST = "SubmitQuest";
        private const string CHANGECUTSCENE = "ChangeCutscene";

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
                    case SPEAKER_TAG:
                        //change speaker name depending on the speaker tag
                        // Noelle only apperas on the left
                        // Other characters only appear on the right
                        leftCharacter.gameObject.SetActiveFast(tagValue.Contains("Noelle"));
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

                        /*
                        if (PORTRAIT_TAG != null)
                        {
                            leftCharacter.PlayAnim(tagValue + "Idle");
                        }*/
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

        public void HandleEvents(string events)
        {
            if (!events.Contains("EVENT"))
                return;
            string[] splits = events.Split(':', 2);
            string lineOne = splits[0].Trim();
            string lineTwo = splits[1].Trim();
            if (lineOne.StartsWith(EVENT))
            {
                ProcessEvent(lineTwo);
            }
        }

        private void ProcessEvent(string eventId)
        {
            if (eventId.StartsWith(SWAPBGM))
            {
                string[] splits = eventId.Split(':', 2);
                string param = splits[1];
                string[] inputs = param.Split(',', 2);
                string new_id = inputs[0];
                string old_id = inputs[1];
                AudioManager.Instance.Stop(old_id);
                AudioManager.Instance.Play(new_id);
            }
            else if (eventId.StartsWith(PLAYBGM))
            {
                string[] splits = eventId.Split(':', 2);
                string id = splits[1];
                AudioManager.Instance.Play(id);
            }
            else if (eventId.StartsWith(ADDQUEST))
            {
                string[] splits = eventId.Split(':', 2);
                string questID = splits[1];
                QuestManager.instance.AddQuest(questID);
            }
            else if (eventId.StartsWith(SUBMITQUEST))
            {
                string[] splits = eventId.Split(':', 2);
                string questID = splits[1];
                QuestManager.instance.SubmitQuest(questID);
            }
            else if (eventId.StartsWith(CHANGECUTSCENE))
            {
                string[] splits = eventId.Split(':', 2);
                string SceneName = splits[1];
                DialogueManager.instance.EndStory();
                SceneManager.LoadScene(SceneName);
                Debug.Log("scene changed to " + SceneManager.GetActiveScene());
            }
        }

        public void SetSpeaker(string name)
        {
            //change speaker name depending on the speaker tag
            // Noelle only apperas on the left
            // Other characters only appear on the right
            leftCharacter.gameObject.SetActiveFast(name.Contains("Noelle"));
            leftCharacter.SetName(name);
            rightCharacter.gameObject.SetActiveFast(name != "Noelle" && name != "Narrator");
            rightCharacter.SetName(name);
            PlayAudio(name == "Narrator" ? "Narrator" : name);
            // DialogueManager
            //     .Instance()
            //     .DialogueAudioPlayer.SetCurrentAudioInfo(name == "Narrator" ? "Narrator" : name);
        }

        public void PlayAudio(string audioName)
        {
            DialogueManager.Instance().DialogueAudioPlayer.SetCurrentAudioInfo(audioName);
        }
    }
}
