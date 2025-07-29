using System;
using Cinemachine;
using Kuroneko.UtilityDelivery;
using UnityEngine;

[Serializable]
public class CameraData{ // Change this to class with methods
    public enum CameraAngle {LowAngle, MidAngle, HighAngle, MidAngleWide};
    public CameraAngle angle;
    public GameObject camera;

    public CinemachineVirtualCamera GetVirtualCamera(){
        return camera.GetComponent<CinemachineVirtualCamera>();
    }

    // Insert player to follow and look at
    public void SetPlayer(Transform playerTransform){
        CinemachineVirtualCamera vc = GetVirtualCamera();
        //vc.LookAt = playerTransform;
        vc.Follow = playerTransform;
    }

    // Activate and deactivate camera
    public void SetCameraActive(bool active){
        camera.SetActiveFast(active);
    }
}