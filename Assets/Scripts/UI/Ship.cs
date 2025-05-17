using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static bool isNearPlayer = false;
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
            isNearPlayer = true;
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            uiPanel.SetActive(false);
        }
    }
}
