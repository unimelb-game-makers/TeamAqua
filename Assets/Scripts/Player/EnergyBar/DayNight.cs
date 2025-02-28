using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    [SerializeField] public Light directionalLight; 
    [SerializeField] public Color targetColor = Color.black;
    [SerializeField] public float duration = 2f;

    private Color initialColor;

    public static bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f; 
   

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

    private void Update()
    {
        if (shouldChangeColor &&　firstTime)
        {
            lerpTime += Time.deltaTime / duration; 
            directionalLight.color = Color.Lerp(initialColor, targetColor, lerpTime);

            if (lerpTime >= 1f) 
            {
                directionalLight.color = targetColor;
                firstTime = false;
            }
        }
    }
}
