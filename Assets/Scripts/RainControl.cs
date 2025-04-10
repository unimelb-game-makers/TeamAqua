using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] public ParticleSystem rainParticles;

    private void Start()
    {
        if (rainParticles != null)
        {
            rainParticles.Stop(); // Ensure it's stopped at the start

        }
    }

    public　void StartRain(float rainDuration) 
    {
        System.Random r = new System.Random();
        int randomValue = r.Next(100);
        if (randomValue <= 30) {
            StartCoroutine(PlayParticlesCoroutine(rainDuration));

        }
        else {
            StartCoroutine(StopParticlesCoroutine());
        }

    }

    private IEnumerator PlayParticlesCoroutine(float rainDuration)
    {
        yield return null; // Wait for the next frame
        if (rainParticles != null)
        {
            rainParticles.Play();
            yield return new WaitForSeconds(rainDuration);
            rainParticles.Stop();
        }
    }

    private IEnumerator StopParticlesCoroutine()
    {
        yield return null; // Wait for the next frame to ensure it's on the main thread
        if (rainParticles != null)
        {
            rainParticles.Stop(); // Stop the particles
        }
    }



    
    
}

