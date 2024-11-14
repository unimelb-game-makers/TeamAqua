using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfo", order = 1)]
public class DialougeAudioInfo : ScriptableObject
{
    public string id;
    public AudioClip[] dialogueTypingSounds;
    [Range(1, 5)]
    public int AudioFrequency = 2;

    [Range(-3, 3)]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    public float maxPitch = 3f;

    public bool StopAudioSource;
    
}
