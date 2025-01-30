using System.Collections;
using System.Collections.Generic;
using Kuroneko.AudioDelivery;
using Kuroneko.UtilityDelivery;
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
            StartCoroutine(ChangeSound(fadeSpeed, delayTrack, "SUNEATER", "FLUTTERING_CRITTER"));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ChangeSound(fadeSpeed, delayTrack, "FLUTTERING_CRITTER", "SUNEATER"));
        }
    }

    public IEnumerator ChangeSound(float FadeTime, float delay, string id, string old_id) 
    {
        // id is the id used to identify each track
        // fades the volume gradually over {FadeTime} amount of time, 
        // and then wait for {delay} amount of time, and then switches to new audio

        //if want abrupt switch, set FadeTime and delay to 0
        float startVolume = Source.volume;
        // fade out volume
        while (Source.volume > 0) {
            Source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        // switch track
        Source.Stop();
        ServiceLocator.Instance.Get<IAudioService>().Stop(old_id);
        Source.volume = startVolume;
        //ServiceLocator.Instance.Get<IAudioService>().
        ServiceLocator.Instance.Get<IAudioService>().Play(id);
        //Source.clip = newClip;
        //Source.PlayDelayed(delay);    <- playdelayed wont work anymore, may need to use start coroutine in tandem with audio delivery plug-in
    }
}
