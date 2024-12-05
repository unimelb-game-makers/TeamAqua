using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
    public float speed = 3;
    public Vector3[] waypoints;
    private int currentIndex = 0;
    private float threshold = 0.1f;
    public void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.Log("no waypoints set");
            return;
        }

        Vector3 targetWaypoint = waypoints[currentIndex];
        if (Vector3.Distance(transform.position, targetWaypoint) <= threshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
    }
    
}
