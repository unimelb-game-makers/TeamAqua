using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerControl : MonoBehaviour
{
    public GameObject TrigPoints;
    public TextAsset inkJSON;
    public int dialogueTypeID;
    public string dialogueTrigger;
    Story story;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        //set the child trigger points to inactive, if pre-chase convo yet to be started
        // for future uses, if no initial convo exists, just set the dialogue trig variable to anything but empty
        /*if (story.variablesState[dialogueTrigger].ToString() == "")
        {
            Debug.Log("trigger points locked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            TrigPoints.SetActive(false);
        }
        else
        {
            Debug.Log("trigger points unlocked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            TrigPoints.SetActive(true);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (story.variablesState[dialogueTrigger].ToString() != "")
        {
            Debug.Log("trigger points unlocked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            TrigPoints.SetActive(true);
            if (other.CompareTag("Player"))
            {
                Debug.Log("entering parent dialogue trigger, onwards are child triggers");
                DialogueSystem.GetDial().EnterDialogueMode(inkJSON, dialogueTypeID);
            }
        }
        else
        {
            Debug.Log("trigger points locked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            TrigPoints.SetActive(false);
        }
    }
}
