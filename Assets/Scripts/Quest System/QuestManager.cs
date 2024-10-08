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
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    void Start()
    {
        // TODO: load the quests from the file when game starts
        // quests = ...
        // finished = ...
        //
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !DialogueSystem.DialMana.dialogueIsPlaying)  //added bool check to see if dialogue is on
        {
            if (!questCanvas.activeSelf)
            {
                questCanvas.SetActive(true);
                Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
                isScaled = false;
                Time.timeScale = 0; // pause the game when the quest canvas is active
            }
            
            else
            {
                Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
                questCanvas.SetActive(false);
                Time.timeScale = 1; // resume game when quest canvas is deactivated
            }
            DrawText();
        }

        if (DialogueSystem.DialMana.dialogueIsPlaying) // forcibly closes questlog if player enters dialogue
        {
            questCanvas.SetActive(false);
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
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     AddQuest(1);
        //     AddQuest(2);
        // }
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     CompleteStep(1, 1);
        // }
    }

    //=============================== Singleton Methods (Accessible from other scripts) ================================//

    /**
        AddQuest: This function adds a quest to the quest list. It reads the quest from the .json file and adds it to the list.
        Parameters: int id - the id of the quest to add
        Return: void
    */
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
    public void CompleteStep(int id, int step)
    {
        step = step - 1; // convert to 0-based index
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].id == id)
            {
                if (quests[i].current_step_number == step)
                {
                    quests[i].current_step_number++;
                    if (quests[i].current_step_number == quests[i].quest_steps.Count)
                    {
                        RemoveQuest(id);
                    }
                    DrawText();
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
    private void RemoveQuest(int id)
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
}
