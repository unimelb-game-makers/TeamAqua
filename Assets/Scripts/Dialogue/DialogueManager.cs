using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText; 
    [SerializeField] private TextMeshProUGUI DisplayNameText;


    [Header("dialogue Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Quest Choices UI")]
    [SerializeField] private GameObject[] QuestChoices;
    public GameObject ButtonPanel;
    private TextMeshProUGUI[] choicesText;
    private TextMeshProUGUI[] QuestChoicesText;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string QUEST_TAG = "quest";
    private static DialogueManager DialMana;

 




 
    //ALL CHANGES MOVED TO DialogueSystem; this is an old version, can prob delete this script








    private void Awake()
    {
        if (DialMana != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        DialMana = this;
    }


    public static DialogueManager GetDial()
    {
        return DialMana;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        QuestChoicesText = new TextMeshProUGUI[QuestChoices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

        
    }

    public void EnterDialougeMode (TextAsset inkJSON)
    {
        currentStory = new Story (inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialText.text = "";
    }

    private void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            // set text for current dialogue
            dialText.text = currentStory.Continue();
            // display choices
            DisplayChoices();
            // handle tags in ink
            HandleTags(currentStory.currentTags);
            
        }
        
        else
        {      
            
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
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

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    DisplayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    Debug.Log("portrait is " + tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout is " + tagValue);
                    break;
                case QUEST_TAG:
                    ButtonPanel.SetActive(false);
                    DisplayQuestChoices();
                    break;
                default:
                    Debug.LogWarning("tag came in but is not currently being handled: " + tag);
                    break;
                
            }
        }

        
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //check if UI can support number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("More choices given than UI could support. Num of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            ButtonPanel.SetActive(true);

            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }


        for (int i = index; i < choices.Length; i++)
        {
            ButtonPanel.SetActive(false);
            choices[i].gameObject.SetActive(false);
        }
    }

    private void DisplayQuestChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //check if UI can support number of choices coming in
        if (currentChoices.Count > QuestChoices.Length)
        {
            Debug.Log("More quest choices given than UI could support. Num of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            //ButtonPanel.SetActive(true);

            QuestChoices[index].gameObject.SetActive(true);
            QuestChoicesText[index].text = choice.text;
            index++;
        }


        for (int i = index; i < QuestChoices.Length; i++)
        {
            //ButtonPanel.SetActive(false);
            QuestChoices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}
