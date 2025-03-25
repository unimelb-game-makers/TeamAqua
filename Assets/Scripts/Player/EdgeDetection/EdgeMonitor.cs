
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeMonitor : MonoBehaviour
{
    public PlayerController playerController;
    private List<EdgeSensor> sensors = new List<EdgeSensor>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(EdgeSensor sensor in GetComponentsInChildren<EdgeSensor>()){
            sensors.Add(sensor);
            sensor.StartSensor();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(EdgeSensor sensor in sensors){
            sensor.FollowPlayer(playerController.transform.position, playerController.saveDirection);
        }
        //Debug.Log(playerController.saveDirection);
    }
}
