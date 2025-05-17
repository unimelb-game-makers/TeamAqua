using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;
public class DayManager : MonoBehaviour
{
    private const float LIGHT_DURATION = 1.0f;

    [Header("Scriptable Objects")] [SerializeField]
    private PlayerSave playerSave;

    [Header("Night Settings")]
    [SerializeField] private Color targetColor = Color.black;
    
    // Exposed Actions
    public static Action<int> OnDayChanged;

    public ParticleSystem rainParticle;
    public const float RAINCHANCE = 0.3f;

    private Coroutine rainRoutine;

    // Current Day and Night
    private int _currentDay = 1;

    private int CurrentDay
    {
        get => _currentDay;
        set
        {
            if (_currentDay == value)
            {
                return;
            }
            _currentDay = value;
            PlayerPrefs.SetInt("currentDay", _currentDay);
            PlayerPrefs.Save();
        }
    }

    private bool isNight = false;
    
    // Serialized Variables
    private PlayerController _playerController;
    private bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f;
    private Color initialColor;
    private Light directionalLight;

    
    private void Start()
    {
        // TODO(Alex): We naively assume that there is only one light source for now.
        directionalLight = FindFirstObjectByType<Light>();
        _playerController = FindFirstObjectByType<PlayerController>();
        initialColor = directionalLight.color;
        EnergyManager.OnEnergyChanged += CheckEnergy;
    }

    private void CheckEnergy(float energyAmount)
    {
        if (energyAmount < 50f && !isNight)
        {
            SetNight(true);
        }
    }

    private void FixedUpdate()
    {
        if (isNight && Input.GetKeyDown(KeyCode.E) && Ship.isNearPlayer)
        {
            StartNewDay();
        }
        
        if (shouldChangeColor && firstTime)
        {
            ChangeLight();
        }
    }

    private void StartNewDay()
    {
        OnDayChanged?.Invoke(CurrentDay);
        CurrentDay += 1;
        SetNight(false);
        _playerController.handleNextDay();
        RainWithDelay(false, 2.0f);
        float randomValue = Random.value;
        if (randomValue < RAINCHANCE)
        {
            RainWithDelay(true, 2.0f); //wait 2s, so rain starts after screen blacked out 
            Debug.Log("Rain starts!");
        }
        else {
            Debug.Log("No rain");
        }
        EnergyManager.instance.OnNextDay();
        
        // Trigger Save whenever a new day is started
        playerSave.Save();
    }

    private void SetNight(bool value)
    {
        isNight = value;
        if (isNight)
        {
            shouldChangeColor = true;
            lerpTime = 0f;
        }
        else
        {
            firstTime = false;
            directionalLight.color = initialColor;
        }
    }

    private void ChangeLight()
    {
        lerpTime += Time.deltaTime / LIGHT_DURATION;
        directionalLight.color = Color.Lerp(initialColor, targetColor, lerpTime);
        if (lerpTime >= 1f)
        {
            directionalLight.color = targetColor;
            firstTime = false;
        }
    }

    private void OnDestroy()
    {
        EnergyManager.OnEnergyChanged -= CheckEnergy;
    }

    [Button]
    public void StartRain()
    {
        if (rainParticle != null && !rainParticle.isPlaying)
        {
            rainParticle.Play();
        }
    }

    public void StopRain()
    {
        if (rainParticle != null && rainParticle.isPlaying)
        {
            rainParticle.Stop();
        }
    }

    public void RainWithDelay(bool startRain, float delay)
    {
        if (rainRoutine != null)
        {
            StopCoroutine(rainRoutine);
        }

        rainRoutine = StartCoroutine(RainAfterDelay(startRain, delay));
    }

    private IEnumerator RainAfterDelay(bool startRain, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(startRain) 
        {
            StartRain();
        }
        else
        {
            StopRain();
        }
    }

}
