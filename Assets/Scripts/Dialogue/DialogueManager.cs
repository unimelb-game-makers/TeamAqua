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
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    public GameObject ButtonPanel;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }


    private static DialogueManager DialMana;

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

            dialText.text = currentStory.Continue();
            DisplayChoices();
            
        }
        else
        {
            
            ExitDialogueMode();
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
            //ButtonPanel.SetActive(true);

            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }


        for (int i = index; i < choices.Length; i++)
        {
            //ButtonPanel.SetActive(false);
            choices[index].gameObject.SetActive(false);
        }
    }
}
