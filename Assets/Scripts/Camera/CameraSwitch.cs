using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public int switchTo = 1;
    public int switchBack = 0;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            CameraManager.cameraManager.SetCamera(switchTo);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            CameraManager.instance.SetCamera(switchBack);
        }
    }
}
