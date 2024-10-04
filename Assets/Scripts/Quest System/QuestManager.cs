using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public List<QuestData> quests = new List<QuestData>();
    public Quest finised;

    public TextMeshProUGUI questText;

    public TextAsset jsonFile;

    public RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   

        //==============for debugging/testing purposes========================//
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddQuest(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveQuest(1);
        }
    }

    public void AddQuest(int id)
    {
        // look for the quest in the .json file
        // add the quest to the quest list
        Quest quest = JsonUtility.FromJson<Quest>(jsonFile.text);
        if (quest != null)
        {
            foreach (QuestData questData in quest.quests)
            {
                // check if the quest is already in the list
                for (int i=0; i<quests.Count ; i++)
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
        if (quests.Count >= 3) {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 300);
        }
    }

    public void RemoveQuest(int id)
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
        if (quests.Count >= 3) {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 300);
        }
        
    }

    private void DrawText() {
        if (quests.Count == 0)
        {
            questText.text = "No Quests Available, press \"L\" to add a quest";
        } else {
            questText.text = "_________________________" + "\n\n";
            for (int i = 0; i < quests.Count; i++)
            {
                if (!quests[i].finished)
                {
                    // write the quest into the questText
                    questText.text = questText.text + "\n" + quests[i].title + "\n\n" + quests[i].description + "\n\n" + "Current Task: " + quests[i].quest_steps[quests[i].current_step_number].description + "\n\n"+ "              Objective: " + quests[i].quest_steps[quests[i].current_step_number].objective + "\n\n";
                    Debug.Log(quests[i].quest_steps[quests[i].current_step_number].description);
                    if (quests[i].quest_steps[quests[i].current_step_number].quest_item_id != -1)
                    {
                        questText.text = questText.text + "              Item: " + quests[i].quest_steps[quests[i].current_step_number].quest_item_id + "\n\n";
                        questText.text = questText.text + "              Amount: " + quests[i].quest_steps[quests[i].current_step_number].quest_item_amount + "\n\n";
                    }
                    questText.text = questText.text + "\n\n" + "Reward: " + quests[i].reward.exp + " exp " + quests[i].reward.gold + " gold" + "\n\n" + "_________________________" + "\n\n";
                }
            }
        }
        
    }

    // public static void CompleteStep(int id, int step)
    // {
    //     for (int i=0; i<quests.count; )
    // }

}
