using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NPCWander : State
{
    [SerializeField] State idleState;
    [SerializeField] State delayIdleState;
    [SerializeField] AnimState animState;
    public float speed = 3;
    public Transform[] waypoints;
    private int currentIndex = 0;
    private float threshold = 0.1f;

    public override void Enter()
    {
        Debug.Log("Entering Wander State");
        
        if(animState != null)
            animState.playAnim();
    }
    public override void Exit(){
        Debug.Log("Exiting Wander State");
    }
    public override void Process()
    {
        Vector3 curWaypoint = waypoints[currentIndex].position;
        Vector3 curPosition = statemachine.transform.position;

        /*Changing States*/
        if(Input.GetKeyDown(KeyCode.Space)){
            statemachine.ChangeState(idleState);
        }

        /*Wandering Logic*/
        if (waypoints.Length == 0)
        {
            Debug.Log("no waypoints set");
            return;
        }

        Vector3 targetWaypoint = new Vector3(curWaypoint.x, 0, curWaypoint.z);
        Vector3 curPos = new Vector3(curPosition.x, 0, curPosition.z);

        if (Vector3.Distance(curPos, targetWaypoint) <= threshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            statemachine.ChangeState(delayIdleState);
        }
        statemachine.transform.position = Vector3.MoveTowards(curPosition, targetWaypoint, speed * Time.deltaTime);

        //Change direction that the sprite is facing
        if(curPos.x < targetWaypoint.x){ //Face right
        statemachine.transform.rotation = Quaternion.Euler(0,180,0);
        }else{ //Face Left
            statemachine.transform.rotation = Quaternion.identity;
        }

    }
    public override void TriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            statemachine.ChangeState(idleState);
        }
    }
}
