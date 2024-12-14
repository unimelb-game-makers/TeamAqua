using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class QuestLocationTrigger : MonoBehaviour
{
    public Ink.Runtime.Story story;
    private bool LocationReached;

    public int id;
    public int step;
    // Start is called before the first frame update
    void Start()
    {
        
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
                story.variablesState["quest_id" + id] = "YES";
                LocationReached = true;
            }
        }
        LocationReached = false;
    }
}
