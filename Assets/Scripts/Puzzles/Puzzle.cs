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
    public List<PushBlock> pushBlocks = new List<PushBlock>();
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

    private void ResetPuzzle(){
        foreach(var pb in pushBlocks){
            pb.transform.position = pb.startPos;
        }
    }

    private bool isFinished(){
        //Check all Switches are on
        foreach(var _switch in switches){
            if(!_switch.On)
                return false;
        }
        return true;
    }

    public void TryOpenDoor(){
        //If all switches are on, then open the door
        if(isFinished() && door)
            LeanTween.moveLocalY(door, -0.3f, 0.5f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = true;
            puzzleCam.gameObject.SetActive(true);
            AudioManager.Instance.Play("PUZZLE_ENTER");
            //AudioManager.Instance.SwapBGM("id", 5);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerEntered = false;
            puzzleCam.gameObject.SetActive(false);
            AudioManager.Instance.Stop("PUZZLE_ENTER");
            
            //Reset the puzzle if it is not completed
            if(!isFinished())
                ResetPuzzle();
        }
    }
}
