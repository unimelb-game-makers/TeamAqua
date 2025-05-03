using System.Collections;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    public ParticleSystem rainParticle;
    public const float RAINCHANCE = 0.3f;
    private Coroutine stopRoutine;

    public static RainControl Instance;


    void Awake()
    {
        Instance = this;
    }

    public void StartRain()
    {
        if (rainParticle != null && !rainParticle.isPlaying)
        {
            rainParticle.Play();
        }
    }

    public void StartRainRandom()
    {
        float randomValue = Random.value;
        if (randomValue < RAINCHANCE)
        {
            StartRain();
            Debug.Log("Rain starts!");
        }
        else {
            Debug.Log("Non rain");
        }
    }

    public void StopRain()
    {
        if (rainParticle != null && rainParticle.isPlaying)
        {
            rainParticle.Stop();
        }
    }

    public void StopRainWithDelay(float delay)
    {
        if (stopRoutine != null)
        {
            StopCoroutine(stopRoutine);
        }

        stopRoutine = StartCoroutine(StopRainAfterDelay(delay));
    }

    private IEnumerator StopRainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopRain();
    }
}