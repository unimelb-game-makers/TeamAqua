using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;
public class DayNight : MonoBehaviour
{
    [SerializeField] public Light directionalLight; 
    [SerializeField] public Color targetColor = Color.black;
    [SerializeField] public static float duration = 1f;

    private Color initialColor;

    public static bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f; 
    public static Action<float, float> OnDayChange;
    public static int currentDay = 1;

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
        lerpTime += Time.deltaTime / duration; 
        directionalLight.color = Color.Lerp(initialColor, targetColor, lerpTime);

        if (lerpTime >= 1f) 
        {
            directionalLight.color = targetColor;
            firstTime = false;
        }
    }

    private void Update()
    {
        if (shouldChangeColor &&　firstTime)
        {
            ChangeLight();
        }
        if (shouldChangeColor && Input.GetKeyDown(KeyCode.E) 
            && Ship.isPlayerShip)
        {
            currentDay++;
            OnDayChange?.Invoke(0f, 1f);
            LeanTween.delayedCall(5f, () => OnDayChange?.Invoke(1f, 0f));
        }
        
    }
}
