using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script is to save game performance and avoid running too many objects at once
    Each scene will have a list of scenes to deactivate and activate.
    When the player enters a new scene, it will close and open the respective scenes.
    
    Can specify whether to turn on the fake mesh or the real mesh depending on what the 
    player should look at and interact with.
*/

[Serializable]
public struct SwitchNode{
    public TerrainNode terrainNode;
    public bool toggleFakeMesh;
}

public class TerrainSwitch : MonoBehaviour
{
    public SwitchNode[] openScenes;
    public SwitchNode[] closeScenes;

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;
        
        OpenScenes();
        CloseScenes();
    }

    public void ToggleScenes(SwitchNode[] switchNodes, bool status){
        foreach(SwitchNode node in switchNodes){
            if(node.toggleFakeMesh == true){
                node.terrainNode.ActiveFake(status);
            } else{
                node.terrainNode.ActiveReal(status);
            }
        }
    }
    public void CloseScenes(){
        ToggleScenes(closeScenes, false);
    }
    public void OpenScenes(){
        ToggleScenes(openScenes, true);
    }
}
