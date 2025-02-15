using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;     // <- caused an error with the Debug statements
using Unity.VisualScripting;
using UnityEngine;

public class NPCIdle : NPCState
{
    [SerializeField] State wanderState;

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        PlayStateAnimation();
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
