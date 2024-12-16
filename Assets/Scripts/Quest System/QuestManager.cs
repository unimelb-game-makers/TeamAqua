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

public class QuestManager : UIState
{
    // Singleton instance
    public static QuestManager instance;

    [SerializeField] private GameObject questCanvas; // the canvas that displays the quests
    private List<QuestData> quests = new List<QuestData>(); // list of all the quests the player has
    private List<QuestData> finised = new List<QuestData>(); // list of all the quests the player has finished
    // TODO: when saving the game, save these lists to a file
    
    [SerializeField] private TextMeshProUGUI questText; // the text that displays the quests
    [SerializeField] private TextAsset jsonFile; // the .json file that contains the quests

    [SerializeField] private RectTransform rt; // the rect transform of the questText
    [SerializeField] private RectTransform Scroll_View_rect_transform; // the rect transform of the scroll view
    //private bool isScaled = false;
    public bool QuestCompleted;
    //public bool questOpen = false;

    [SerializeField] UIState All_UI_Off;
    public UIState paused;
    private bool isScaled = false;
    
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

    public override void UIEnter()
    {
        Debug.Log("Entering questOn State");
        questCanvas.SetActive(true);
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        isScaled = false;
        Time.timeScale = 0; // pause the game when the quest canvas is active
        
    }
    public override void UIProcess()
    {
        DrawText();
        /*Changing States*/
        if(Input.GetKeyDown(KeyCode.J)){
            UIstatemachine.ChangeUIState(All_UI_Off);
            Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1);
            isScaled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UIstatemachine.ChangeUIState(All_UI_Off);
            UIstatemachine.ChangeUIState(paused);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CompleteStep(1,2);
        }

        
       
        // if (questCanvas.activeSelf && !isScaled)
        // {
        //     Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0, 0.05f, 0);
        //     if (Scroll_View_rect_transform.localScale.y >= 1)
        //     {
        //         isScaled = true;
        //     }
        // }
    }

    void Update()
    {
        if (questCanvas.activeSelf && !isScaled)
        {
            Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0, 1f, 0);
            Debug.Log("Scaling up");
            if (Scroll_View_rect_transform.localScale.y >= 1)
            {
                isScaled = true;
            }
        }

        // Debugging/testing purposes
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddQuest(1);
            AddQuest(2);
            
        }
    }
    /*  ========================================= Migrated to UIstatemachine ======================================================
    void Start()
    {
        // TODO: load the quests from the file when game starts
        // quests = ...
        // finished = ...
        //
        //QuestCompleted = false;
        OnDisableQ();        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && UIinputProvider.instance().UI_canOpen[2] && !DialogueSystem.GetIsPlaying())  //added bool check to see if dialogue is on
        {
            if (!questCanvas.activeSelf)
            {
                OnEnableQ();
            }
            
            else
            {
                OnDisableQ();
            }
            DrawText();
        }

        if (DialogueSystem.GetIsPlaying() || PausePanelScript.instance().isPaused) // forcibly closes questlog if player enters dialogue
        {
            OnDisableQ();
        }

        

        // Debugging/testing purposes
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddQuest(1);
            AddQuest(2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //CompleteStep(1, 1);
        }
    }

    //=============================== Singleton Methods (Accessible from other scripts) ================================//

    /**
        AddQuest: This function adds a quest to the quest list. It reads the quest from the .json file and adds it to the list.
        Parameters: int id - the id of the quest to add
        Return: void
    
    public void OnEnableQ()
    {
        //JournalManager.instance().journalCanvas.SetActive(false);
        questCanvas.SetActive(true);
        UIinputProvider.instance().SendUIinput(2);
        questOpen = true;
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        isScaled = false;
        Time.timeScale = 0; // pause the game when the quest canvas is active
    }

    public void OnDisableQ()
    {
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        questCanvas.SetActive(false);
        UIinputProvider.instance().SendUIinput(0);
        questOpen = false;
        Time.timeScale = 1; // resume game when quest canvas is deactivated
    }
    */
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
                        break;
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
        if (quests.Count > 0)
        {
            Debug.Log("Scaling up222");
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 350);
        }

        DrawText();
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
        //check if quest type is location
        if (quests[id-1].quest_steps[step-1].objective_type == "LOCATION")
        {
            // check if the quest step's active status is true
            if (quests[id-1].quest_steps[step-1].active == true)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckStatus(int id, int step, Ink.Runtime.Story story)
    {  
        // checking if quest log is empty
        Debug.Log("number of quests is " + quests.Count);
        // checking if quest objective type is gather
        if (quests.Count != 0)
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
                        story.variablesState["quest_id" + id] = "YES";
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
                    DrawText();
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
                DrawText();
            }
        }
    }

    //===============================================================================================================//

  /**
        DrawText: This function draws the quests in the UI. It displays the title, description, current task, objective, item, amount, and reward of each quest.
        Parameters: none
        Return: void
    */

    
    public void DrawText() 
    {
        if (quests.Count == 0)
        {
            questText.text = "No Quests, You're All Done!";
        }
        else
        {
            questText.text = "_________________________" + "\n\n";
            for (int i = 0; i < quests.Count; i++)
            {
                if (!quests[i].finished)
                {
                    // write the quest into the questText
                    questText.text += "\n" 
                    + quests[i].title + "\n\n" 
                    
                    // Make the description text smaller using <size> tag
                    + quests[i].description + "\n\n" 
                    
                    + "Task: " 
                    + quests[i].quest_steps[quests[i].current_step_number].description + "\n\n"
                    + "Step: " + (quests[i].current_step_number + 1) + "/" + quests[i].quest_steps.Count + "\n\n\n\n"
                    
                    + "<color=yellow>Current Objective:</color>" + "\n\n"  // Objective in yellow
                    +  quests[i].quest_steps[quests[i].current_step_number].objective + "\n\n";
                    
                    if (quests[i].quest_steps[quests[i].current_step_number].quest_item_id != -1)
                    {
                        // TODO: replace the item ID with the actual item name after implementing the inventory system and items etc
                        questText.text += "Item: " + quests[i].quest_steps[quests[i].current_step_number].quest_item_id + "\n\n";
                        questText.text += "Amount: " + quests[i].quest_steps[quests[i].current_step_number].quest_item_amount + "\n\n";
                    }
                    
                    questText.text += "\n\n" 
                    + "<color=green>Reward: " + quests[i].reward.exp + " exp " + quests[i].reward.gold + " gold</color>" 
                    + "\n\n" + "_________________________" + "\n\n";
                }
            }
        }
    }
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
        DrawText();
        if (quests.Count > 1)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 350);
        }
    }

    public List<QuestData> GetQuests()
    {
        return quests;
    }

    public override void UIExit()
    {
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        Debug.Log("Exiting QuestON State");
    }
}
