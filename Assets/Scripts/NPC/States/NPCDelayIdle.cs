using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDelayIdle : NPCState
{
    [SerializeField] State wanderState;
    [SerializeField] State idleState;
    [SerializeField] float waitSeconds;

    public override void Enter()
    {
        if(waitSeconds != 0){
            StartCoroutine(delayIdle());
        }
        
        PlayStateAnimation();
    }

    public override void TriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            statemachine.ChangeState(idleState);
        }
    }

    /*A Coroutine if the npc is only to be idle for a specific amount of time*/
    public IEnumerator delayIdle(){
        yield return new WaitForSeconds(waitSeconds);
        statemachine.ChangeState(wanderState);
    }
}
