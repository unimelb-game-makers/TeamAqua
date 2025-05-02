using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueAudioPlayer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] public DialougeAudioInfo defaultAudioInfo;
    [SerializeField] public DialougeAudioInfo[] audioInfos;
    public DialougeAudioInfo currentAudioInfo;
    private Dictionary<string, DialougeAudioInfo> audioInfoDictionary;
    public AudioSource audioSource;

    private bool HashApproach = true; //-> set to true if want predictable-ish dialogue speech
    // Start is called before the first frame update


    //===========================REFRACTORING STILL IN PROCESS============================================

    // Audio-related stuffs below
    public void InitializeAudioDictionary()
    {   //sets up audio dictionary, map each char to an audio frequency
        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
        
        audioInfoDictionary = new Dictionary<string, DialougeAudioInfo> { { defaultAudioInfo.id, defaultAudioInfo } };
        foreach (DialougeAudioInfo audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    public void SetCurrentAudioInfo(string id)
    {   // plays the audio associated with the given id
        DialougeAudioInfo audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.Log("failed to find audio info for id: " + id);
        }
    }

    public void PlayDialogueSound(int currentDisplayedCharCount, char currentCharacter)
    {   // plays the audio for [number] of characters that loads on the screen

        // set variables for the below based on config
        AudioClip[] dialogueTypingSounds = currentAudioInfo.dialogueTypingSounds;
        int AudioFrequency = currentAudioInfo.AudioFrequency;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool StopAudioSource = currentAudioInfo.StopAudioSource;

        // play sound based on config
        if (currentDisplayedCharCount % AudioFrequency == 0)
        {
            if (StopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;

            //creating predictable speech by hashcode
            if (HashApproach)
            {   
                //generate hashcode for each characters
                int hashcode = currentCharacter.GetHashCode();
                //sound clip
                int predictableIndex = hashcode % dialogueTypingSounds.Length;
                soundClip = dialogueTypingSounds[predictableIndex];
                //pitch
                int minPitchInt = (int) (minPitch * 100);
                int maxPitchInt = (int) (maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                
                //cant divide by 0, no range so skip selection
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashcode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }

                else
                {       //set pitch to either minPitch or maxPitch
                    audioSource.pitch = minPitch;
                }

            }
            else
            {
                //sound clips
                int randomIndex = Random.Range(0, dialogueTypingSounds.Length);
                soundClip = dialogueTypingSounds[randomIndex];
                //pitch
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }
            
            //play sounds
            audioSource.PlayOneShot(soundClip);
        }
    }

    public void ExitAudio()
    {
        //exits out of audio, to use in exit dialogue mode
        SetCurrentAudioInfo(defaultAudioInfo.id);
        audioSource.Stop();
    }
}
