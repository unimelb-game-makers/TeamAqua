using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueChoices : MonoBehaviour
{

    [Header ("Choices")]
    [SerializeField] public GameObject[] choiceButton;
    private Story currentStory;

    public bool displaying = false;
    public static DialogueChoices DialChoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        DialChoice = this;
    }
    public static DialogueChoices Instance()
    {
        return DialChoice;
    }

    public void DisplayChoices(Story currentStory)
    {
        ClearChoices(currentStory);
        displaying = true;

        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];
            choiceButton[i].SetActive(true);
            choiceButton[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            // Add listener for the button
            int choiceIndex = i;
            choiceButton[i].GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex, currentStory));
        }
    }

    // When a choice is selected, continue the story with the chosen option
    public void OnChoiceSelected(int choiceIndex, Story currentStory)
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
                //steven's change below, needs more testing
               if (selectedChoice.tags[i].Contains("finish")) {
                    int questID = int.Parse(selectedChoice.tags[i].Substring(7));
                    Debug.Log("Quest ID: " + questID);

                    // finishes the quest upon interaction
                    if (questID > 0)
                    {
                        Debug.Log("Removing quest");
                        NPCDialogue.instance().HasQuest = false;    // not working rn, will wait for quest-inventory integration
                        QuestManager.instance.RemoveQuest(questID);
                    }
               }

               if (selectedChoice.tags[i].Contains("done")) {
                    StartCoroutine(DialogueSystem.GetDial().ExitDialogueMode());
               }
            }
        }

        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        
        DialogueSystem.GetDial().ContinueStory();
    }

    // Clears all the current choice buttons
    public void ClearChoices(Story currentStory)
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

}
