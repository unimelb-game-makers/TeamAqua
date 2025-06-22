using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script is to save game performance and avoid running too many objects at once
    Each scene will have a list of scenes to deactivate and activate.
    When the player enters a new scene, it will close and open the respective scenes.
*/

public class TerrainScene : MonoBehaviour
{
    public TerrainScene[] openScenes;
    public TerrainScene[] closeScenes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
