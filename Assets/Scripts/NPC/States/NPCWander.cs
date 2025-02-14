using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.PostProcessing;

public class NPCWander : NPCState
{
    [SerializeField] State idleState;
    [SerializeField] State delayIdleState;
    public float speed = 3;
    public Transform[] waypoints;
    private int currentIndex = 0;
    private float threshold = 0.1f;

    //Dynamically updated variables
    Vector3 targetWaypoint;
    Vector3 curPosition;

    public override void Enter()
    {
        PlayStateAnimation();
    }
    public override void Process()
    {
        targetWaypoint = waypoints[currentIndex].position;
        curPosition = statemachine.transform.position;

        /*Wandering Logic*/
        if (waypoints.Length == 0)
        {
            Debug.Log("no waypoints set");
            return;
        }

        if (distanceBetween(curPosition, targetWaypoint) <= threshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            statemachine.ChangeState(delayIdleState);
        }
        statemachine.transform.position = Vector3.MoveTowards(curPosition, targetWaypoint, speed * Time.deltaTime);

        //Change direction that the sprite is facing
        if(curPosition.x < targetWaypoint.x){ //Face right
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

    //Neglect the y distance
    private float distanceBetween(Vector3 v1, Vector3 v2){
        return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
    }
}
