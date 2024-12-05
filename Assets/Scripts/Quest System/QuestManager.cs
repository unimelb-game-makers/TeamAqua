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

    [SerializeField] private GameObject questCanvas; // the canvas that displays the quests
    private List<QuestData> quests = new List<QuestData>(); // list of all the quests the player has
    private List<QuestData> finised = new List<QuestData>(); // list of all the quests the player has finished
    // TODO: when saving the game, save these lists to a file

    [SerializeField] private TextMeshProUGUI questText; // the text that displays the quests
    [SerializeField] private TextAsset jsonFile; // the .json file that contains the quests

    [SerializeField] private RectTransform rt; // the rect transform of the questText
    [SerializeField] private RectTransform Scroll_View_rect_transform; // the rect transform of the scroll view
    private bool isScaled = false;
    public bool QuestCompleted;
    public bool questOpen = false;

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

        if (questCanvas.activeSelf && !isScaled)
        {
            Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0, 0.05f, 0);
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
    */
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
    public void AddQuest(int id)
    {
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
                        return;
                    }
                }
                if (questData.id == id && !quests.Contains(questData) && !questData.finished)
                {
                    QuestCompleted = false;     //quest status is false at the start
                    questData.active = true;
                    questData.current_step_number = 0;
                    quests.Add(questData);
                }
            }
        }
        DrawText();
        if (quests.Count > 1)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 350);
        }
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
    public void CompleteStep(int id, int step, Ink.Runtime.Story story, bool objective = false)
    {
        if (quests != null)
        {
            // check for quest type
            if (!objective) {
                if (quests[id-1].quest_steps[step-1].quest_item_id != -1) {
                    // check if the player has the item in their inventory
                    Debug.Log("Checking for item in inventory");
                    if (inventory.HasItem(quests[id-1].quest_steps[step-1].quest_item_id, quests[id-1].quest_steps[step-1].quest_item_amount)) {
                        print ("Item found in inventory" + (quests[id-1].quest_steps[step-1].quest_item_id));

                        // remove the item from the inventory
                        inventory.RemoveItem(quests[id-1].quest_steps[step-1].quest_item_id, quests[id-1].quest_steps[step-1].quest_item_amount);
                        story.variablesState["quest_id" + id] = "YES";
                    } else {
                        Debug.Log("item not found in inventory");
                        return;
                    }
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

    
    private void DrawText() 
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
            {                              //in the DialogueSystem script, this quest status is manually turned true, 
                QuestCompleted = false;    //<-- return the quest status to false after its removal, so other quests arent affected
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

}
