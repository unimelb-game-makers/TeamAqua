using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherManager : MonoBehaviour
{
    // Start is called before the first frame update

    private RainControl rainControl;
    public static WeatherManager Instance;
    public static Action<float> OnWeatherChanged;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
            rainControl = GetComponent<RainControl>();
            
    }

    private void Start() {
        OnWeatherChanged += rainControl.StartRain;
    }

    private void DestroyOnWeather() {
        OnWeatherChanged -= rainControl.StartRain;
    }
}
