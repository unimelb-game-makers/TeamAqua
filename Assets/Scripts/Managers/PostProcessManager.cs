using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    public PostProcessVolume volume;

    public Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
