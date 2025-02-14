using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;     // <- caused an error with the Debug statements
using Unity.VisualScripting;
using UnityEngine;

public class NPCIdle : NPCState
{
    [SerializeField] State wanderState;

    //dialogue stuffs below
    [SerializeField] public TextAsset inkJSON;
    [SerializeField] public int DialogueTypeID;     //0 is default dialogue, 1 is second dialogue mode

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        PlayStateAnimation();
    }
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override void Process()
    {
        if(Input.GetKeyDown(KeyCode.E) && !DialogueSystem.GetIsPlaying())
        {
            DialogueSystem.GetDial().EnterDialogueMode(inkJSON, DialogueTypeID);
        }
    }


    public override void TriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            statemachine.ChangeState(wanderState);
        }
    }
}
