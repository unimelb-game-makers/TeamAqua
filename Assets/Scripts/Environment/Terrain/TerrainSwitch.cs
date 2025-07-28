using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script is to save game performance and avoid running too many objects at once
    Each scene will have a list of scenes to deactivate and activate.
    When the player enters a new scene, it will close and open the respective scenes.
    
    This is controlled by the TerrainSignal scripts held inside the children of the Switch Gameobject.
*/
public class TerrainSwitch : MonoBehaviour
{
    public GameObject meshObject;

    private void Start() {
        foreach(TerrainSignal signal in GetComponentsInChildren<TerrainSignal>()){
            signal._switch = this;
        }
    }

    public void SwitchTerrain(TerrainSignal.Status status){
        meshObject.SetActive(status == TerrainSignal.Status.ENTERED);
    }
}
