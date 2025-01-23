using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    public static SoundChanger instance;
    public AudioSource Source; // the original track
    public AudioClip newClip;   // the replacement track
    public float fadeSpeed;
    public float delayTrack;     // the time delay before new track is played

/*=======DEV NOTE: maybe use scriptable objects to have ids on the tracks, so we can use strings to load whichever track we want
without having to declare a public audioclip newClip. We might also be able to store data like volume, pitch, each track's unique properties (if they have) in there as well
*/
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
            StartCoroutine(ChangeSound(fadeSpeed, delayTrack));
        }
    }

    public IEnumerator ChangeSound(float FadeTime, float delay) 
    {
        // fades the volume gradually over {FadeTime} amount of time, 
        //and then wait for {delay} amount of time, and then switches to new audio
        //if want abrupt switch, set FadeTime and delay to 0
        float startVolume = Source.volume;
        // fade out volume
        while (Source.volume > 0) {
            Source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        // switch track
        Source.Stop();
        Source.volume = startVolume;
        Source.clip = newClip;
        Source.PlayDelayed(delay);
    }
}
