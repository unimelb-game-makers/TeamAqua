using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainNode : MonoBehaviour
{
    public GameObject meshObject;
    private TerrainSwitch terrainSwitch;

    private void Start() {
        terrainSwitch = GetComponentInChildren<TerrainSwitch>();
        if (terrainSwitch.meshObject == null)
            terrainSwitch.meshObject = meshObject;
    }
}
