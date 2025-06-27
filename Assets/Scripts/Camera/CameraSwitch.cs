using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CameraData.CameraAngle switchTo;
    public CameraData.CameraAngle switchBack = 0;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            CameraManager.instance.SetCamera(switchTo);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            CameraManager.instance.SetCamera(switchBack);
        }
    }
}
