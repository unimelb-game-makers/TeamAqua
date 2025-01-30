using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BgmAudioInfo", menuName = "ScriptableObjects/BgmAudioInfo", order = 1)]
public class BgmAudioInfo : ScriptableObject
{
    public string id;
    public AudioClip[] tracks;
    public float volume;
    public float pitch;
    public bool loop;
    public bool StopAudioSource;
    
}
