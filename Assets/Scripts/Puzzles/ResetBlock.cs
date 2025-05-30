using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBlock : MonoBehaviour
{
    [NonSerialized] public Puzzle puzzle;

    private float timeActivated = 0;
    private bool activated = false;


    private void Update() {
        if(activated == false)
            return;
        // Count the seconds the player is on the switch
        timeActivated += Time.deltaTime;
        Debug.Log($"time = {timeActivated}");
        // Time activated hit 3 seconds
        if(timeActivated >= 3){
            puzzle.ResetPuzzle();
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Player or block activating reset switch
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("SwitchInteractible")){
            activated = true;
        }
        Debug.Log("Registered");
    }
    private void OnTriggerExit(Collider other) {
        // Player or block leaving reset switch
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("SwitchInteractible")){
            activated = false;
            timeActivated = 0;
        }
    }
}
