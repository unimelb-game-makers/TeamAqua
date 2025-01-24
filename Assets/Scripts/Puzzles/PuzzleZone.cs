using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PuzzleZone : MonoBehaviour
{
    public CinemachineVirtualCamera puzzleCam;
    private bool playerEntered = false;

    private void Start() {
        if(puzzleCam.gameObject.activeSelf){
            puzzleCam.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = true;
            puzzleCam.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = false;
            puzzleCam.gameObject.SetActive(false);
        }
    }
}
