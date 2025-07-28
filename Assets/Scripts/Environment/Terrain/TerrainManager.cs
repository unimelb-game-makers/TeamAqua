using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public List<TerrainZone> terrainZones;

    // Start is called before the first frame update
    void Start()
    {
        foreach(TerrainZone zone in GetComponentsInChildren<TerrainZone>()){
            terrainZones.Add(zone);
        }
    }
}
