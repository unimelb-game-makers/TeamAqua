using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCIdle : State
{
    [SerializeField] State wanderState;

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override void TriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            statemachine.ChangeState(wanderState);
        }
    }
}
