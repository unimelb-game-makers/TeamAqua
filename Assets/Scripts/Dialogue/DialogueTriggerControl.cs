using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerControl : MonoBehaviour
{
    public static DialogueTriggerControl inst;
    public GameObject TrigPoints;
    public TextAsset inkJSON;
    public int dialogueTypeID;
    public string dialogueTrigger;
    Story story;
    public bool dialTrigger;


// ===================== REUSABILITY: need dial trigger to be set true for triggers to work ==============================
// ----> if used like Act 0 chase, set to false
// ----> if used alone without any other part/npc convo before, set to true in editor.

    public void Awake()
    {
        inst = this;
    }

    public static DialogueTriggerControl instance()
    {
        return inst;
    }
    // Start is called before the first frame update
    void Start()
    {
        //TrigPoints.SetActive(false);
        if (dialTrigger)
        {
            TrigPoints.SetActive(true);
        }
        
        story = new Story(inkJSON.text);
        //set the child trigger points to inactive, if pre-chase convo yet to be started
        // for future uses, if no initial convo exists, just set the dialogue trig variable to anything but empty
        story.ObserveVariable(dialogueTrigger, (variableName, newValue) =>
        {
            Debug.Log("old variable is " + variableName);
            Debug.Log("new variable is " + newValue);
            dialTrigger = true;
            TrigPoints.SetActive(true);
        });

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("variable state is " + story.variablesState[dialogueTrigger]);   
        if (Input.GetKeyDown(KeyCode.X))
        {
            story.variablesState[dialogueTrigger] = "AAAAAA";
            Debug.Log(story.variablesState[dialogueTrigger]);
        }
    }

    public void Trigger()   //this almost took in string id, but that created a var not delcared bug. so we end up using dialogueTrigger as a string instead 
    {
        story.variablesState[dialogueTrigger] = "yes";
    }

    public void OnTriggerEnter(Collider other)
    {   // bug: currently, the dialogue trigger isnt being transmitted to here, leaving the dialogue triggers constantly locked
        if (dialTrigger)
        {
            Debug.Log("variable state is " + story.variablesState[dialogueTrigger]);   
            Start();
            //Debug.Log("trigger points unlocked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            //TrigPoints.SetActive(true);
            if (other.CompareTag("Player"))
            {
                Debug.Log("entering parent dialogue trigger, onwards are child triggers");
                DialogueSystem.GetDial().EnterDialogueMode(inkJSON, dialogueTypeID);
            }
        }
        return;
    }
}
