using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script is to save game performance and avoid running too many objects at once
    Each scene will have a list of scenes to deactivate and activate.
    When the player enters a new scene, it will close and open the respective scenes.
*/

public class TerrainSwitch : MonoBehaviour
{
    public GameObject[] openScenes;
    public GameObject[] closeScenes;

    [SerializeField] private TerrainSwitch prevSwitch;
    [SerializeField] private TerrainSwitch nextSwitch;
    
    private bool playerPassed = false; // Check if the player has already gone through the current terrain

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;
        
        OpenScenes();
        CloseScenes();
    }

    public void ToggleScenes(GameObject[] scenes, bool status){
        foreach(GameObject scene in scenes){
            scene.SetActive(status);
        }
    }
    public void CloseScenes(){
        ToggleScenes(closeScenes, false);
    }
    public void OpenScenes(){
        ToggleScenes(openScenes, true);
    }
}
