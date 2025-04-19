using UnityEngine;
using System;
using UnityEngine.Assertions;

public class DayCycle : MonoBehaviour
{
    public static Action<int> OnDayChange;

    public int currentDay = 1;

    private bool isNight = false;
    public static Action<bool> OnNightChange;

    public PlayerController playerController;

    private void Start()
    {
        EnergyManager.OnEnergyChanged += CheckEnergy;
        Assert.IsNotNull(playerController, "playerController field is null in DayCycle object");
    }

    private void OnDisable()
    {
        EnergyManager.OnEnergyChanged -= CheckEnergy;
    }

    private void updateIsNight(float energyAmount)
    {
        if (isNight)
        {
            return;
        }

        if (energyAmount < 50)
        {
            isNight = true;
            OnNightChange.Invoke(true);
        }
    }

    private void CheckEnergy(float energyAmount)
    {
        updateIsNight(energyAmount);
        if (energyAmount <= 0)
        {
            StartNewDay();
            return;
        }
    }

    public void StartNewDay()
    {
        OnDayChange?.Invoke(currentDay);
        currentDay = currentDay + 1;

        isNight = false;
        OnNightChange.Invoke(false);

        playerController.handleNextDay();
        EnergyManager.Instance.OnNextDay();
        // EnergyManager.ene
    }

    private void FixedUpdate()
    {
        if (isNight && Input.GetKeyDown(KeyCode.E) && Ship.isNearPlayer)
        {
            StartNewDay();
        }
    }
}
