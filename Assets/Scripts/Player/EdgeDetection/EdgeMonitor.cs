
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
            sensor.StartSensor(playerController);
        }
        Debug.Log(sensors);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(EdgeSensor sensor in sensors){
            sensor.FollowPlayer();
        }
    }
}
