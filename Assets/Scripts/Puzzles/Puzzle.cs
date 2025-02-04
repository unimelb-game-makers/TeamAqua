using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Kuroneko.AudioDelivery;
using Kuroneko.UtilityDelivery;

public class Puzzle : MonoBehaviour
{
    public CinemachineVirtualCamera puzzleCam;
    public GameObject door;
    public Switch[] switches;
    private bool playerEntered = false;

    private void Start() {
        //Turn the Puzzle Camera Off
        if(puzzleCam.gameObject.activeSelf){
            puzzleCam.gameObject.SetActive(false);
        }
        //Set the parent puzzle for each switch
        foreach(var _switch in switches){
            _switch.puzzle = this;
        }
    }

    public void CheckFinished(){
        //Check all Switches are on
        foreach(var _switch in switches){
            if(!_switch.On)
                return;
        }
        //If all switches are on, then open the door
        Debug.Log("Opening Door");
        if(door){
            //door.SetActive(false);
            LeanTween.moveLocalY(door, -0.3f, 0.5f);   
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = true;
            puzzleCam.gameObject.SetActive(true);
            AudioManager.Instance.Play("PUZZLE_ENTER");
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = false;
            puzzleCam.gameObject.SetActive(false);
        }
    }
}
