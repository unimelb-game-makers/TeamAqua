using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    public static SoundChanger instance;
    public AudioSource Source; // the original track
    public AudioClip newClip;   // the replacement track
    public float delay;     // the time delay before new track is played

    void Awake()
    {
        instance = this;    //should there be multiple instances of soundchangers in a scene....
    }

    public static SoundChanger Instance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        Source =  GameObject.Find("AudioSource").GetComponent<AudioSource>();  // get the audio source component from the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangeSound(delay));
        }
    }

    public IEnumerator ChangeSound(float FadeTime) 
    {
        // fades the volume gradually over {delay} amount of time, and then switches to new audio
        float startVolume = Source.volume;

        while (Source.volume > 0) {
            Source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        Source.Stop();
        Source.volume = startVolume;
        Source.clip = newClip;
        Source.PlayDelayed(delay);
    }
}
