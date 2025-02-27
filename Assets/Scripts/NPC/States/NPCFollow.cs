using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : NPCState
{
    [SerializeField] State idleState;
    [SerializeField] State followIdleState;
    public GameObject target;
    public float targetDistance = 1.0f; // How far the NPC will reach the player before stopping
    public float maxDistance = 10.0f;
    public float speed = 1.0f;
    
    Vector3 targetPosition;
    Vector3 curPosition;
    public override void Enter()
    {
        PlayStateAnimation();
    }

    public override void Process()
    {
        /*Wandering Logic*/
        if (target == null)
        {
            Debug.Log("no target set for follow state");
            statemachine.ChangeState(idleState);
            return;
        }

        targetPosition = target.transform.position;
        curPosition = statemachine.transform.position;

        /*Caught up with player, so stop and idle.*/
        if (Vector3.Distance(curPosition, targetPosition) <= targetDistance)
        {
            statemachine.ChangeState(followIdleState);
        }
        /*Player is too far away, so teleport to player. (Likely being blocked by wall)*/
        else if(Vector3.Distance(curPosition, targetPosition) > maxDistance)
        {
            statemachine.transform.position = new Vector3(targetPosition.x - targetDistance, targetPosition.y, targetPosition.z);
            Debug.Log("Teleported NPC");
        }
        /*Else, simply follow the player*/
        else
            statemachine.transform.position = Vector3.MoveTowards(curPosition, targetPosition, speed * Time.deltaTime);

        //Change direction that the sprite is facing
        if(curPosition.x < targetPosition.x){ //Face right
            statemachine.transform.rotation = Quaternion.Euler(0,180,0);
        }else{ //Face Left
            statemachine.transform.rotation = Quaternion.identity;
        }
    }
}