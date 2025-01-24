using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerControl : UIState
{
    public static DialogueTriggerControl inst;
    public GameObject TrigPoints;
    public TextAsset inkJSON;
    public int dialogueTypeID;
    public string dialogueTrigger;
    Story story; //= DialogueSystem.GetDial().currentStory;        //might want to access the story from dialogue sys instead?
    public bool dialTrigger;
    public List<GameObject> trigPoints;
    public GameObject dialoguePanel;


    //Big flaw, if clicke ESC during dialogue trig points, no way to re-enter dialogue mode
    //also might want to merge this witht he trig point script.

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
    public override void UIEnter()
    {
        /*
        foreach(GameObject triggers in GetComponentsInChildren<GameObject>()){
            trigPoints.Add(triggers);
        }*/

        //TrigPoints.SetActive(false);
        if (dialTrigger)
        {
            TrigPoints.SetActive(true);
        }
        
        story = new Story(inkJSON.text);
        //set the child trigger points to inactive, if pre-chase convo yet to be started
        // for future uses, if no initial convo exists, just set the dialogue trig variable to anything but empty

        /*
        story.ObserveVariable(dialogueTrigger, (variableName, newValue) =>
        {   //the purpose of this chunk is now to check for trigger only in the case where its used right after dialogue mode 0 is used 
            Debug.Log("old variable is " + variableName);
            Debug.Log("new variable is " + newValue);
            dialTrigger = true;
            TrigPoints.SetActive(true);
        });
        */

    }

    // Update is called once per frame
    public override void UIProcess()
    {   /*
        if (last index of trig point collided)
        {
            UIExit();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UIstatemachine.ChangeUIState(All_UI_Off);
            StartCoroutine(DialogueSystem.GetDial().ExitDialogueMode());
        }
        //Debug.Log("variable state is " + story.variablesState[dialogueTrigger]);  
         
        if (Input.GetKeyDown(KeyCode.X))
        {
            story.variablesState[dialogueTrigger] = "AAAAAA";
            Debug.Log(story.variablesState[dialogueTrigger]);
        }
        */

        // WIP
        /*
        foreach (GameObject trig in GetComponentsInChildren<GameObject>())
        {
            if (trig.GetComponent<Collider>().CompareTag("Player"))
            {
                trigs
            }
        }
        */

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
            //UIEnter();
            //Debug.Log("trigger points unlocked, dialogue trigger is " + story.variablesState[dialogueTrigger]);
            //TrigPoints.SetActive(true);
            if (other.CompareTag("Player"))
            {
                Debug.Log("entering parent dialogue trigger, onwards are child triggers");
                DialogueSystem.GetDial().EnterDialogueMode(inkJSON, dialogueTypeID);
                //dialoguePanel.SetActive(true);
            }
        }
        return;
    }

    public override void UIExit()
    {
        Debug.Log("exiting dialogue trigger control...");
        dialoguePanel.SetActive(false);
    }
}
