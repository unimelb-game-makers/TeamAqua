using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType 
{
    Kill,
    Collect,
    Talk,
    Location
}

public class Objective : MonoBehaviour
{
    public ObjectiveType objectiveType;

    public int id;

    public int amount;

    public int currentAmount = 0;

    public Vector3 location;


    void Update()
    {
        if (objectiveType == ObjectiveType.Location)
        {
            // get object tagged with player
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (Vector3.Distance(location, player.position) < 2)
            {
                QuestManager.instance.CompleteCurrentStep(id);
                
                Destroy(gameObject);
            }
        }
    }
}