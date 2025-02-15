using System.Collections;
using Kuroneko.AudioDelivery;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public SoundDatabase soundDatabase;
    
    private string _currentBgm = string.Empty;

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
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(SwapBGM("BGM_ISLAND_SUNEATER", 1));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(SwapBGM("BGM_ISLAND_FLUTTERING_CRITTER", 0));
        }
    }

    public IEnumerator SwapBGM(string clipName, float fadeSpeed)
    {
        if ( string.IsNullOrEmpty(_currentBgm) && soundDatabase.TryGetSound(_currentBgm, out Sound current))
        {


            // didnt seem to get pass this if statement

            // Stops the current playing BGM
            AudioSource source = current.config.Get();
            Debug.Log("audio manager: first if statement passed");
            if (source)
            {
                float startVolume = source.volume;
                while (source.volume > 0)
                {
                    source.volume -= startVolume * Time.deltaTime / fadeSpeed;
                    yield return null;
                    Debug.Log("stop playing pls");
                }
            }
            Stop(_currentBgm);
            Debug.Log(_currentBgm + "stopped playing");
        }
        // PLays the next BGM
        // Stop(_currentBgm);
        _currentBgm = clipName;
        Play(_currentBgm);
        Debug.Log("bgm changed to " + _currentBgm );
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
        if (soundDatabase.TryGetSound(clipName, out Sound sound))       //this check passed 2/11/25
            sound.config.Stop(instanceId);
        else
            Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
    }
}
