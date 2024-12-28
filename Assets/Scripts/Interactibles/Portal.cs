using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal nextPortal;
    private bool teleported = false;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && nextPortal && !teleported){
            Debug.Log("Teleported");
            other.gameObject.transform.position = nextPortal.transform.position;
            nextPortal.teleported = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && teleported){
            teleported = false;
        }
    }
}
