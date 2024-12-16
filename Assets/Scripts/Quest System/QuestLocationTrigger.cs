using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class QuestLocationTrigger : MonoBehaviour
{
    public Story story;
    private bool LocationReached;

    public int id;  // the id of the associated quest
    public int step;    // the specific step within that quest
    // Start is called before the first frame update
    public TextAsset inkJSON;
    void Start()
    {
        story = new Story(inkJSON.text);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (QuestManager.Instance().CheckStart(id, step))
        {
            if (other.CompareTag("Player"))
            {
                story.variablesState["quest_id1"] = "YES";
                // this works but VAR trigger seems to fail to reach the actual ink script, prob needs more setup in ink file for the actual VAR to be triggered tho, possibly needs a function call
                Debug.Log("location reached, dialogue trigger switched to " + story.variablesState["quest_id1"]);
                LocationReached = true;
            }
        }
        LocationReached = false;
    }
}
