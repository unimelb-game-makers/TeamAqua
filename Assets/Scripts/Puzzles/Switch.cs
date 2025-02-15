using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Puzzle puzzle;
    public bool On = false;
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("SwitchInteractible")){
            On = true;
            
            //Tell the Puzzle Parent to check all switches.
            if(puzzle)
                puzzle.TryOpenDoor();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("SwitchInteractible")){
            On = false;
        }
    }
}
