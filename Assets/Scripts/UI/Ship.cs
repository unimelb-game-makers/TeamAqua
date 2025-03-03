using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static bool isPlayerShip = false;
    public GameObject uiPanel;
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true; 
        collider.radius = 1f;

        uiPanel.SetActive(false);
    
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure player has "Player" tag
        {
            isPlayerShip = true;
            uiPanel.SetActive(true);
            Debug.Log("Player entered the ship.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerShip = false;
            uiPanel.SetActive(false);
            Debug.Log("Player exited the ship.");
        }
    }
}
