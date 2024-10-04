using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCubeController : MonoBehaviour
{
    public GameObject questManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "Player")
        {
            questManager.SetActive(true);
        }
    }
}
