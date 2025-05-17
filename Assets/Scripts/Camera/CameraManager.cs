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
