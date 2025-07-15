using System.Collections;
using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;

public class TerrainNode : MonoBehaviour
{
    public GameObject fakeMesh; // Reference to the single mesh object of the terrain to reduce performance cost.

    // Toggle the real tilemapped meshes, and deactivate the fake mesh regardless
    public void ActiveReal(bool on){
        fakeMesh.SetActiveFast(false);
        gameObject.SetActiveFast(on);
    }

    // Toggle fake mesh, and deactivate real mesh regardless
    public void ActiveFake(bool on){
        fakeMesh.SetActiveFast(on);
        gameObject.SetActiveFast(false);
    }
}
