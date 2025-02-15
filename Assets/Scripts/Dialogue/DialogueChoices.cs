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
    //private Story currentStory;

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
        DialogueSystem.Instance().ChooseChoice(choiceIndex);
    }

    // Clears all the current choice buttons
    public void ClearChoices(Story currentStory)
    {
        displaying = false;
        //Debug.Log("Clearing choices");
        //Debug.Log(currentStory.currentChoices.Count);
        foreach (GameObject c in choiceButton)
        {
            c.GetComponent<Button>().onClick.RemoveAllListeners();
            c.GetComponentInChildren<TextMeshProUGUI>().text = "";
            c.SetActive(false);

        }
    }

}
