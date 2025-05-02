using System;
using UnityEngine;
using UnityEngine.Assertions;

public class EnergyManager : MonoBehaviour
{
    public const float MAX_ENERGY = 100f;
    public static EnergyManager Instance;
    public static Action<float> OnEnergyChanged;
    public PlayerSave playerSave;

    private float _energyAmount = 100;
    public float energyAmount
    {
        get { return _energyAmount; }
        set
        {
            if (_energyAmount == value)
            {
                return;
            }
            _energyAmount = value;
            PlayerPrefs.SetFloat("energyAmount", _energyAmount);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Assert.IsNotNull(playerSave, "playerSave field is null in EnergyManager object");

        // This is actually bugged :D
        // It saves indiscriminately of the player's inventory and such.
        // float savedEnergyAmount = playerSave.energy;
        // if (savedEnergyAmount == 0)
        // {
        //     _energyAmount = MAX_ENERGY;
        // }
        // else
        // {
        //     _energyAmount = savedEnergyAmount;
        // }
    }

    private void Start()
    {
        OnEnergyChanged?.Invoke(energyAmount);
    }

    // Debug logic
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoseEnergy(20);
        }
    }

    public void LoseEnergy(float loss)
    {
        if (energyAmount == 0)
        {
            return;
        }

        energyAmount = Math.Max(energyAmount - loss, 0);
        OnEnergyChanged?.Invoke(energyAmount);
    }

    public void OnNextDay()
    {
        energyAmount = MAX_ENERGY;
        OnEnergyChanged?.Invoke(energyAmount);
    }

    public void GainEnergy(float healingAmount)
    {
        energyAmount += healingAmount;
        energyAmount = Mathf.Clamp(energyAmount, 0, MAX_ENERGY);
        OnEnergyChanged?.Invoke(energyAmount);
    }
}
