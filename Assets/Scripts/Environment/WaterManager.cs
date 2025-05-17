using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // Turn on the water shader if it is disabled in the scene
        if(!meshRenderer.enabled)
            meshRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
