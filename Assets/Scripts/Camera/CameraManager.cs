using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject[] cameras;

    public int currentCamera = 0;

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
        SetCamera(currentCamera);
    }

    // Set follow and aim for each camera at player
    public void SetPlayer(PlayerController player){
        foreach(GameObject camOBJ in cameras){
            CinemachineVirtualCamera vc = camOBJ.GetComponent<CinemachineVirtualCamera>();
            vc.LookAt = player.transform;
            vc.Follow = player.transform;
        }
    }

    public void SetCamera(int idx){
        currentCamera = idx;
        for(int i = 0; i<cameras.Length; i++){
            if(i == idx)
                cameras[i].SetActive(true);
            else
                cameras[i].SetActive(false);

        }
    }
}
