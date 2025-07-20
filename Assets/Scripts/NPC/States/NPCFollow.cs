using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : NPCState
{
    [SerializeField] State idleState;
    [SerializeField] State followIdleState;
    [SerializeField] SpriteTransformer spriteTransformer;
    public GameObject target;

    private PlayerController pc;
    public float targetDistance = 1.0f; // How far the NPC will reach the player before stopping
    public float maxDistance = 10.0f;
    public float speed = 1.0f;
    public float stepThreshold = 0.2f;
    private bool firstMet = true;
    Vector3 targetPosition;
    Vector3 curPosition;

     private Queue<Vector3> playerTrail; 

    public override void Enter()
    {
        PlayStateAnimation();
        pc = target.GetComponent<PlayerController>();

        if (pc != null)
        {
            playerTrail = pc.playerTrail;
        }
    }

    public override void Process()
    {
        /*Wandering Logic*/
        if (target == null)
        {
            Debug.Log("no target set for follow state");
            statemachine.ChangeState(idleState);
            playerTrail.Clear();
            return;
        }

        if (firstMet)
        {
            playerTrail.Clear();
            firstMet = false;
            Debug.Log("First met successed");
        }

        targetPosition = target.transform.position;
        curPosition = statemachine.transform.position;

        /*Caught up with player, so stop and idle.*/
        if (Vector3.Distance(curPosition, targetPosition) <= targetDistance)
        {
            statemachine.ChangeState(followIdleState);
            Debug.Log("Caught NPC");
            playerTrail.Clear();

        }
        /*Player is too far away, so teleport to player. (Likely being blocked by wall)*/
        else if (Vector3.Distance(curPosition, targetPosition) > maxDistance)
        {
            statemachine.transform.position = new Vector3(targetPosition.x - targetDistance, targetPosition.y, targetPosition.z);
            Debug.Log("Teleported NPC");
            playerTrail.Clear();

        }
        /*Else, simply follow the player*/
        else
            if (playerTrail.Count > 0)
            {
            Vector3 targetPos = playerTrail.Peek();
            statemachine.transform.position = Vector3.MoveTowards(curPosition, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPos) <= stepThreshold)
                {
                    playerTrail.Dequeue();
                }
            }
        //Change direction that the sprite is facing
        if (curPosition.x < targetPosition.x)
        { //Face right
            spriteTransformer.flipX(true);
        }
        else
        { //Face Left
            spriteTransformer.flipX(false);
        }
    }
}