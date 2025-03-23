using System;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTag : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    public NPCDialogue dialogueSource;
    [SerializeField]
    public Story story = null;
    public string questID;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttachStory()
    {
        story = DialogueSystem.Instance().currentStory;
    }
    /*
    public void ObserveQuest()
    {
        story.ObserveVariable(questID, (variableName, newValue)=>
        {
            Debug.Log("old variable is " + variableName);
            Debug.Log("new variable is " + newValue);
        });
    }*/

    public void CheckTag()
    {
        if(story == null) 
        {
            Debug.Log("no storry found");
            return;
        }

        Debug.Log("story connected: " + story);
        Debug.Log("[pre-switch]the quest variable is: " + story.variablesState[questID]);
    
        if(story.variablesState[questID] == "FINISHED")
        {
            dialogueSource.HasQuest = false;
            Debug.Log("[post_switch]the quest variable is: " + story.variablesState[questID]);
        }
    }
}
