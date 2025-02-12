using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DialogueTags : MonoBehaviour
{
    [Header ("Left - Right portraits")]
    [SerializeField] private TextMeshProUGUI charNameLeft;
    [SerializeField] private TextMeshProUGUI charNameRight;
    //[SerializeField] private GameObject[] choiceButton;
    [SerializeField] private GameObject leftDial;
    [SerializeField] private GameObject rightDial;
    private const string ID_TAG = "id";
    private int QuestSid;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    [SerializeField] private Animator portraitAnimLeft;
    [SerializeField] private Animator portraitAnimRight;
    [SerializeField] private Animator cutSceneAnim;
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";
    private const string CUTSCENE_TAG = "cutscene";
    public static DialogueTags dialTags;
    // Start is called before the first frame update
    void Start()
    {
        if(leftDial)
            leftDial.SetActive(false);
        if(rightDial)
            rightDial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        dialTags = this;
    }
    public static DialogueTags Instance()
    {
        return dialTags;
    }

    public void HandleTags(List<string> currentTags)
    {   //this handles all tags aside from quest

        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("error: tag could not be parsed: " + tag);
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
                case "questS":      //---> this handles dialogue-based quest giving
                    QuestSid = int.Parse(tagValue);
                    QuestManager.instance.AddQuest(QuestSid);   
                    Debug.Log("questS working on C#-end");  
                    break;
                case SPEAKER_TAG:   //change speaker name depending on the speaker tag 
                    //charName.text = tagValue;
                    /*
                    if(SceneManager.GetActiveScene().name == "Cutscene 1")
                    {
                        if (tagValue == "Noelle")   // player's portrait on left hand side
                        {
                            charNameLeft.text = tagValue;
                            //Debug.Log(tagValue);
                        }
                        else if (tagValue == "Narrator")    // Narrator doesnt have any portraits or name tag, just empty
                        {
                            continue;
                        }
                        else      // every other characters' portraits on right hand side
                        {
                            charNameRight.text = tagValue;
                            //Debug.Log(tagValue);
                        }
                        break;
                    }
                    else */
                    {
                        if (tagValue == "Noelle")   // player's portrait on left hand side
                        {
                        
                            leftDial.SetActive(true);
                            rightDial.SetActive(false);
                            charNameLeft.text = tagValue;
                            //Debug.Log(tagValue);
                        }
                        else if (tagValue == "Narrator")    // Narrator doesnt have any portraits or name tag, just empty
                        {
                            leftDial.SetActive(false);
                            rightDial.SetActive(false);
                        }
                        else      // every other characters' portraits on right hand side
                        {           
                            leftDial.SetActive(false);
                            rightDial.SetActive(true);
                            charNameRight.text = tagValue;
                            //Debug.Log(tagValue);
                        }
                        break;
                    }      

                case PORTRAIT_TAG:  //change speaker portrait depending on portrait tag
                    if (tagValue == "Noelle")
                    {
                        portraitAnimLeft.Play(tagValue);
                    }

                    else
                    {
                        portraitAnimRight.Play(tagValue);
                    }
                    
                    //Debug.Log("portrait is " + tagValue);
                    break;
                case AUDIO_TAG:
                    DialogueAudioManager.GetAudioMana().SetCurrentAudioInfo(tagValue);
                    break;
                default:
                    Debug.LogWarning("tag came in but is not currently being handled: " + tag);
                    break;
                


                case CUTSCENE_TAG:  //change speaker portrait depending on portrait tag
                    cutSceneAnim.Play(tagValue);
                    Debug.Log("cutscene frame is " + tagValue);
                    break;
            }
        }        
    }
}
