using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This works under the Terrain Switch.
    Simply detects if the player has entered or exited and returns that value to the Terrain Switch.
    Enter -> Open the scene
    Exit -> Close the scene
*/

public class TerrainSignal : MonoBehaviour
{
    public enum Status {ENTERED, EXITED};

    [SerializeField] Status status;
    [NonSerialized] public TerrainSwitch _switch;


    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;
        // Send signal to switch
        if (_switch)
            _switch.SwitchTerrain(status);
    }
}
