using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : State
{
    [SerializeField] State idleState;
    public float speed = 3;
    public Vector3[] waypoints;
    private int currentIndex = 0;
    private float threshold = 0.1f;

    public override void Enter()
    {
        Debug.Log("Entering Wander State");
    }
    public override void Exit(){
        Debug.Log("Exiting Wander State");
    }
    public override void Process()
    {
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

        Vector3 targetWaypoint = waypoints[currentIndex];
        if (Vector3.Distance(statemachine.transform.position, targetWaypoint) <= threshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
        statemachine.transform.position = Vector3.MoveTowards(statemachine.transform.position, targetWaypoint, speed * Time.deltaTime);
    }
    public override void TriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            statemachine.ChangeState(idleState);
        }
    }
}
