using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;
public class DayNight : MonoBehaviour
{
    [SerializeField] public Light directionalLight; 
    [SerializeField] public Color targetColor = Color.black;
    private static readonly float  LIGHT_DURATION = 1.0f;

    private Color initialColor;

    public static bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f; 
    public static Action<float, float> OnDayChange;

    public static float previousDay = 1;
    public static float nextDay = 2;

    private void Start()
    {
        initialColor = directionalLight.color; 
    }

    private void OnEnable()
    {
        EnergyManager.OnEnergyChanged += CheckEnergy;
    }

    private void OnDisable()
    {
        EnergyManager.OnEnergyChanged -= CheckEnergy;
    }

    private void CheckEnergy(float energyAmount)
    {
        if (energyAmount < 50)
        {
            shouldChangeColor = true;
            lerpTime = 0f; 
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

    public static void StartNewDay()
    {
        previousDay = nextDay;
        nextDay++;
        WeatherManager.OnWeatherChanged?.Invoke();
    }

    private void Update()
    {
        if (shouldChangeColor &&　firstTime)
        {
            ChangeLight();
        }
        if (shouldChangeColor && Input.GetKeyDown(KeyCode.E) 
            && Ship.isNearPlayer)
        {
            OnDayChange?.Invoke(previousDay, nextDay);
        }
        
    }
}
