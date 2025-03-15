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
        // Subscribe RainChange() to WeatherManager's event
        WeatherManager.OnWeatherChanged += RainChance;
        
        if (rainParticles != null)
        {
            rainParticles.Stop(); // Ensure it's stopped at the start
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        WeatherManager.OnWeatherChanged -= RainChance;
    }

    public　void RainChance() 
    {
        System.Random r = new System.Random();
        int randomValue = r.Next(100);
        if (randomValue <= 15) {
            rainParticles.Play();
        }
        else {
            rainParticles.Stop();
        }

    }
    
}
