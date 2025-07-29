using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    // Camera Manager Variables
    public static CameraManager instance;

    public CameraData[] cameras;

    public CameraData.CameraAngle currentAngle;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start() 
    {
        SetCamera(currentAngle);
    }

    // Set follow and aim for each camera at player
    public void SetPlayer(PlayerController player){
        foreach(CameraData camOBJ in cameras){
            camOBJ.SetPlayer(player.transform);
        }
    }

    // Go through the cameras and activate the one we want, and deactivate the rest.
    public void SetCamera(CameraData.CameraAngle angle){
        currentAngle = angle;
        for(int i = 0; i<cameras.Length; i++){
            if(cameras[i].angle == angle)
                cameras[i].SetCameraActive(true);
            else
                cameras[i].SetCameraActive(false);
        }
    }
}
