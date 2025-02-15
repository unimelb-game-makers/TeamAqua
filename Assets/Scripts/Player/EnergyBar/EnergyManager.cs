using System;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public const float MAX_ENERGY = 100f;
    public static EnergyManager Instance;
    public static Action<float> OnEnergyChanged;
    
    private float energyAmount = 100;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        energyAmount = MAX_ENERGY;
    }

    private void Start()
    {
        OnEnergyChanged?.Invoke(energyAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoseEnergy(20);
        }
    }
    
    public void LoseEnergy (float loss)
    {
        energyAmount -= loss;
        OnEnergyChanged?.Invoke(energyAmount);
    }

    public void GainEnergy ( float healingAmount)
    {
        energyAmount += healingAmount;
        energyAmount = Mathf.Clamp(energyAmount, 0, MAX_ENERGY);
        OnEnergyChanged?.Invoke(energyAmount);
    }
}
