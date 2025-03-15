using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherManager : MonoBehaviour
{
    // Start is called before the first frame update

    public WeatherManager Instance;
    public static Action OnWeatherChanged;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

}
