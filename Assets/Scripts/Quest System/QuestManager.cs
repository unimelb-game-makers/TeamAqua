/**
    Title:          QuestManager.cs
    Author:         Bill Zhu
    Verison:        1.0
    Last Updated:   2024-10-05
    Description:   This is a singleton class responsible for quest management
                    in the game. It reads quests from a .json file and adds them
                    to a list of quests. It also keeps track of the quests the player
                    has finished. The class provides functions to add quests, complete
                    steps in quests, and draw the quests in the UI.

                    To use the functions:

                    1. QuestManager.AddQuest(int id): This function adds a quest to the quest list. It reads the quest from the .json file and adds it to the list. Add it to NPC's etc.
                        - or simply use the QuestGiver script by attaching it to an object
                    
                    2. QuestManager.CompleteStep(int id, int step): This function completes a specific step in a quest and moves to the next step. Use it when the player completes a task in the quest.

                    - everything else is done automatically by the QuestManager
    Edited by:
    Steven, 11/6/2024: 
        - added bool QuestComplete for testing of MoveKnots function in DialogueSystem
            + if QuestComplete == true: move to chunk of dialogue that plays after quest is complete
            + if QuestComplete == false: move to chunk of dialogue that plays when quest is incomplete but player tries to interact with npc
        - issues: 
            + might raise confusion between different quest ID, 
            + assuming player can ONLY complete a quest upon interacting with an NPC again, this means that technically, player can only complete one quest at a time (cant interact with multiple NPC at the same time, maybe, but even if we could it would probably be a scripted encounter and we would eliminiate the option to complete quest before this multi-NPC interaction plays out)   --->  so the bool check QuestComplete could still work?
        - fix: preferably, check for quest completion by quest[id].finished instead but steven has skill issue and does not know how to implement that without making a big mess rn



    Steven, 12/11/2024:
    - much progress on dialogue - quest integration, made CheckStatus function to access a quest's finished status (may be modified later to be a quest'step's finished status)
    - pretty much just a copy and modification of CompleteStep, but i thought it would be better to separate the logic at the time as CompleteStep is still being worked on, so when its ready, ill modify CheckStatus and combine both functions
    - it works fine if quest objective type is GATHER
    - I lost the vertical scrolling when making a questOn state because the rt recttransform is accessed here in addquest() and removequest(), i reckon vertical scrolling will be recovered if we turn this entire quest manager into a state, but im not doing that yet as this script is still being worked on extensively
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Parsed;

public class QuestManager : MonoBehaviour
{
    // Singleton instance
    public static QuestManager instance;

    private List<QuestData> quests = new List<QuestData>(); // list of all the quests the player has
    private List<QuestData> finised = new List<QuestData>(); // list of all the quests the player has finished
    // TODO: when saving the game, save these lists to a file
    
    [SerializeField] private TextAsset jsonFile; // the .json file that contains the quests

    public bool QuestCompleted;
    //public bool questOpen = false;

    [SerializeField] private Inventory inventory; // the inventory of the player
    
    void Awake()
    {
        
        // Singleton setup
        if (instance == null)
        {
            instance = this; // Set the instance to this instance of QuestManager
        }
        else
        {
            Destroy(gameObject); // Ensure that there's only one instance of the QuestManager
        }
    }
    public static QuestManager Instance()
    {
        return instance;
    }
    public void AddQuest(int id)
    {
        Debug.Log("number of quests is " + quests.Count);
        Quest quest = JsonUtility.FromJson<Quest>(jsonFile.text);
        if (quest != null)
        {
            foreach (QuestData questData in quest.quests)
            {
                // check if the quest is already in the list
                for (int i = 0; i < quests.Count; i++)
                {
                    if (quests[i].id == id)
                    {
                        Debug.Log("Quest already in list");
                        break;  // -> turning return into break seems to make it multiple of the same quest can be added to the list
                    }
                }
                if (questData.id == id && !quests.Contains(questData) && !questData.finished)
                {
                    Debug.Log("Adding quest: " + questData.title);
                    QuestCompleted = false;     //quest status is false at the start
                    questData.active = true;
                    questData.current_step_number = 0;
                    quests.Add(questData);
                }
            }
        }
        
        Debug.Log("number of quests is " + quests.Count);
    }

    /**
        CompleteStep: This function completes a specific step in a quest and moves to the next step.
        Parameters: int id - the id of the quest, int step - the step number to complete
        Return: void
    */

    /*
    public void CheckStep(int id, int step, bool objective = false, bool QuestCon = false)
    {
        probably takes in id and steps and story?
        check in inventory or elsewhere for completion status of a quest
        if quest status is completed,
            story.variablestate("quest_id" + id) = "YES"
        else:
            break
    }
    */

    public bool CheckStart(int id, int step)
    {
        // returns whether a quest step has been started or not, mostly used for quest type LOCATION
        //check if quest log is empty
        if (quests.Count != 0)
        {
            Debug.Log("quest count passed, checking for quest step type...");
            //check if quest type is location
            if (quests[id-1].quest_steps[step-1].objective_type == "LOCATION")
            {
                Debug.Log("quest type is location");
                // check if the quest step's active status is true
                if (quests[id-1].quest_steps[step-1].active == true)
                {
                    Debug.Log("quest step status is ACTIVE");
                    return true;
                }
                Debug.Log("quest step status was not active...");
                return false;
            }
            Debug.Log("quest type wasnt location");
            return false;
        }
        Debug.Log("quest count was 0, no quests yet");
        return false;
    }
    public bool CheckStatus(int id, int step, Ink.Runtime.Story story)
    {  
        // checking if quest log is empty
        Debug.Log("number of quests is " + quests.Count);
        // checking if quest objective type is gather
        if (quests.Count != 0 && id >= quests.Count)
        {
            Debug.Log("quest obj type is " + quests[id-1].quest_steps[step - 1].objective_type);
            switch (quests[id-1].quest_steps[step-1].objective_type)
            {   
                // TYPE : GATHER
                case "GATHER":
                    Debug.Log("QUEST TYPE IS GATHER");
                    //if (quests[id-1].quest_steps[step-1].quest_item_id != -1) 
                
                    // check if the player has the item in their inventory
                    Debug.Log("Checking for item in inventory");
                    if (inventory.HasItem(quests[id-1].quest_steps[step-1].quest_item_id, quests[id-1].quest_steps[step-1].quest_item_amount)) {
                        print ("Item found in inventory" + (quests[id-1].quest_steps[step-1].quest_item_id));
                        quests[id-1].quest_steps[step-1].finished = true;
                        Debug.Log("Item found, setting quest status to finished");
                        // remove the item from the inventory
                        story.variablesState["quest_id" + id] = "FINISHED";
                        Debug.Log("quest_id" + id);
                        Debug.Log(story.variablesState["quest_id" + id]);   // set off trigger in ink to move to a different part of the story
                        return true;
                    } 
                    else {
                        Debug.Log("item not found in inventory, quest status remains unfinished");
                        quests[id-1].finished = false;
                        return false;
                    }

                // TYPE : LOCATION
                case "LOCATION":

                    Debug.Log("QUEST TYPE IS LOCATION");
                    /* approach: probably set invisible collider at the target location, each with their own ID (scriptable object?).if collided, then come back here to set story.variablestate("quest_id" + id) = "YES"
                    */
                    if (QuestLocationTrigger.instance().LocationReached)    // bool set off upon reaching designated location
                    {
                        Debug.Log("location reached, setting off dialogue trigger variable");
                        story.variablesState["quest_id" + id] = "FINISHED";  // set off dialogue var to move on to submitquest
                        return true;
                    }
                    Debug.Log("quest mana: location not reached, bool still false");
                    return false;
                    //break;

                // TYPE : TALK
                case "TALK":
                    Debug.Log("QUEST TYPE IS TALK");
                    /* approach: place ink tag at the end of the dictated convo with the relevant NPC(s), if parses through said tag then come back here to set story.variablestate("quest_id" + id) = "YES"
                    */
                    return false;
                    //break;
            }
            Debug.Log("objective type was not GATHER (-1 detected)");
            return false;
        }
        Debug.Log("no quests found in quest log ");
        return false;
    }
        
    public void CompleteStep(int id, int step, bool objective = false)
    {
        
        // check for quest type
        if (!objective){
            if (quests[id-1].quest_steps[step-1].quest_item_id != -1) {
                // check if the player has the item in their inventory
                Debug.Log("Checking for item in inventory");
                if (inventory.HasItem(quests[id-1].quest_steps[step-1].quest_item_id, quests[id-1].quest_steps[step-1].quest_item_amount)) {
                    print ("Item found in inventory" + (quests[id-1].quest_steps[step-1].quest_item_id));

                    // remove the item from the inventory
                    inventory.RemoveItem(quests[id-1].quest_steps[step-1].quest_item_id, quests[id-1].quest_steps[step-1].quest_item_amount);
                    //story.variablesState["quest_id" + id] = "YES";
                } else {
                    Debug.Log("item not found in inventory");
                    return;
                }
                
            }
        }
        // if (objective) {
        //     if (quests[id-1].)
        // }

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].id == id)
            {
                Debug.Log("Quest ID: " + id);
                Debug.Log("Quest Step: " + step);
                Debug.Log("Current Step: " + quests[i].current_step_number);
                if (quests[i].current_step_number == step-1)
                {
                    Debug.Log("Step: " + step);
                    quests[i].current_step_number++;
                    if (quests[i].current_step_number == quests[i].quest_steps.Count)
                    {
                        //QuestCompleted = true;      //---> likely to change, QuestCompleted should be toggled true only when player has fulfilled all quest steps and hasnt submitted quest yet, and then after they submit it and RemoveQuest(id) is called, turn it back to false
                        RemoveQuest(id);
                    }
                }
            }
        }
    }

    public void CompleteCurrentStep(int id)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].id == id)
            {
                quests[i].current_step_number++;
                if (quests[i].current_step_number == quests[i].quest_steps.Count)
                {
                    //QuestCompleted = true;      //---> likely to change, QuestCompleted should be toggled true only when player has fulfilled all quest steps and hasnt submitted quest yet, and then after they submit it and RemoveQuest(id) is called, turn it back to false
                    RemoveQuest(id);
                }
            }
        }
    }

    //===============================================================================================================//

  /**
        DrawText: This function draws the quests in the UI. It displays the title, description, current task, objective, item, amount, and reward of each quest.
        Parameters: none
        Return: void
    */

    public void RemoveQuest(int id)     //--note: was previously private, not public
    {
        for (int i = quests.Count - 1; i >= 0; i--)
        {
            if (quests[i].id == id)
            {                              
                quests[i].finished = true;
                quests.RemoveAt(i);
            }
        }
    }

    public List<QuestData> GetQuests()
    {
        return quests;
    }
}
