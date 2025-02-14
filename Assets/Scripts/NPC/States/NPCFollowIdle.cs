using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// TO BE TREATED AS AN EXTENSION OF NPCFOLLOW
public class NPCFollowIdle : NPCState
{
    // Enter idle.
    // Check if the follow threshhold is overpassed and enter the follow state
    [SerializeField] NPCFollow followState;

    public override void Enter()
    {
        PlayStateAnimation();
    }
    public override void Process()
    {
        //If no target, do nothing for now.
        if(followState.target == null)
            return;
        
        if (Vector3.Distance(statemachine.transform.position, followState.target.transform.position) > followState.targetDistance)
        {
            statemachine.ChangeState(followState);
        }
    }
    public override void TriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && followState.target == null){
            followState.target = other.gameObject;
        }
    }
}
