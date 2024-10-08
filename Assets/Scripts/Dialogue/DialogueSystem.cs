using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private GameObject[] choiceButton;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private const string SPEAKER_TAG = "speaker";

    public string speaker_name = "";
    public static DialogueSystem DialMana;

    public bool displaying = false;

    private void Awake()
    {
        if (DialMana != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        DialMana = this;
    }

    public static DialogueSystem GetDial()
    {
        return DialMana;
    }

    public static void SetSpeakerName(string name)
    {
        DialMana.speaker_name = name;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        charName.text = speaker_name + "\n============================================";
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !displaying && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }

        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Time.timeScale = 0;
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        Time.timeScale = 1;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialText.text = "";
        ClearChoices(); // Clear choice buttons on exit
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialText.text = currentStory.Continue();

            if (currentStory.currentChoices.Count > 0) {
                DisplayChoices();
            } else {
                ClearChoices();
            }

        }else
        {
            ExitDialogueMode();
        }
    }

    // Displays the choice buttons
    private void DisplayChoices()
    {
        ClearChoices();
        displaying = true;

        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];
            choiceButton[i].SetActive(true);
            choiceButton[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            // Add listener for the button
            int choiceIndex = i;
            choiceButton[i].GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }
    }

    // When a choice is selected, continue the story with the chosen option
    private void OnChoiceSelected(int choiceIndex)
    {
        // Retrieve the selected choice
        Choice selectedChoice = currentStory.currentChoices[choiceIndex];

        // Check if the selected choice has the "quest" tag
        if (selectedChoice.tags != null)
        {
            for (int i = 0; i < selectedChoice.tags.Count; i++)
            {
               if (selectedChoice.tags[i].Contains("quest")) {
                    // for substring 6
                    int questID = int.Parse(selectedChoice.tags[i].Substring(6));
                    Debug.Log("Quest ID: " + questID);

                    // give quest to player
                    if (questID > 0)
                    {
                        Debug.Log("Adding quest");
                        QuestManager.instance.AddQuest(questID);
                    }
               }
            }
        }

        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        
        ContinueStory();
    }


    // Clears all the current choice buttons
    private void ClearChoices()
    {
        displaying = false;
        Debug.Log("Clearing choices");
        Debug.Log(currentStory.currentChoices.Count);
        foreach (GameObject c in choiceButton)
        {
            c.GetComponent<Button>().onClick.RemoveAllListeners();
            c.GetComponentInChildren<TextMeshProUGUI>().text = "";
            c.SetActive(false);

        }
    }

    public static bool GetIsPlaying()
    {
        return DialMana.dialogueIsPlaying;
    }
}
