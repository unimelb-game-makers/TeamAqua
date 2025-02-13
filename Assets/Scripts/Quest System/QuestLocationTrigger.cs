using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting;

public class QuestLocationTrigger : MonoBehaviour
{
    public Story story;
    public static QuestLocationTrigger questLocationTrigger;
    public bool LocationReached;

    public int id;  // the id of the associated quest
    public int step;    // the specific step within that quest
    // Start is called before the first frame update
    public TextAsset inkJSON;
    void Awake()
    {
        questLocationTrigger = this;
    }

    public static QuestLocationTrigger instance()
    {
        return questLocationTrigger;
    }
    void Start()
    {
        story = DialogueSystem.GetDial().currentStory;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider hit, checking quest step status... ");
        if (QuestManager.Instance().CheckStart(id, step))   //location off-limit if quest step not acive yet.
        {
            if (other.CompareTag("Player"))
            {
                //DialogueSystem.GetDial().currentStory.variablesState["quest_id" + id] = "YES";      // has to be passed through to the dialogue system
                // this works but VAR trigger seems to fail to reach the actual ink script, prob needs more setup in ink file for the actual VAR to be triggered tho, possibly needs a function call
                // Issue qith quest manager: indexing doesnt work if quests are added out of order, if u add quest 10 as your first quest, it will throw index 9 at the quest list but since its the very first quest, the index should be 0
                //story = DialogueSystem.GetDial().currentStory;
                //QuestManager.Instance().CheckStatus(id,step, story);
                Debug.Log("location reached, dialogue trigger " + "quest_id" + id + " switched to " + DialogueSystem.GetDial().currentStory.variablesState["quest_id" + id]);
                LocationReached = true;
                return;
            }
            LocationReached = false;
            return;
        }
        LocationReached = false;
        return;
    }
}
