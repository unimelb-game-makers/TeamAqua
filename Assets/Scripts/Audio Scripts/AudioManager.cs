using System.Collections;
using System.Collections.Generic;
using Kuroneko.AudioDelivery;
using Kuroneko.UtilityDelivery;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource Source; // the original track
    public AudioClip newClip;   // the replacement track
    public float fadeSpeed;
    public float delayTrack;     // the time delay before new track is played
    public AudioMixerGroup MixerBgm;
    public SoundDatabase soundDatabase;
    private string currentBgm = string.Empty;

/*=======DEV NOTE: maybe use scriptable objects to have ids on the tracks, so we can use strings to load whichever track we want
without having to declare a public audioclip newClip. We might also be able to store data like volume, pitch, each track's unique properties (if they have) in there as well


2/2/25
functions todo

Play()

Swap()


making changed to audio delivery code to add fade out
*/
    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Source =  GameObject.Find("AudioSource").GetComponent<AudioSource>();  // get the audio source component from the scene
        Source = MixerBgm.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayBGM("SUNEATER");
        }
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
            Debug.Log("fading out audio");
            yield return null;
        }
        Stop(old_id);
        Source.volume = startVolume;
        Play(id);
    }
    
    public void PlayBGM(string clipName)
    {
        if (string.IsNullOrEmpty(currentBgm))
        {
            Stop(currentBgm);
        }

        currentBgm = clipName;
        Play(currentBgm);
    }
    
    public void Play(string clipName, string instanceId = "")
    {
        if (soundDatabase.TryGetSound(clipName, out Sound sound))
            sound.config.Play(instanceId);
        else
            Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
    }

    public void Pause(string clipName, string instanceId = "")
    {
        if (soundDatabase.TryGetSound(clipName, out Sound sound))
            sound.config.Pause(instanceId);
        else
            Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
    }

    public void Resume(string clipName, string instanceId = "")
    {
        if (soundDatabase.TryGetSound(clipName, out Sound sound))
            sound.config.Resume(instanceId);
        else
            Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
    }

    public void Stop(string clipName, string instanceId = "")
    {
        if (soundDatabase.TryGetSound(clipName, out Sound sound))
            sound.config.Stop(instanceId);
        else
            Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
    }
}
