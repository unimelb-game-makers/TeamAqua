using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager energyManager;
    public Image RemainingEnergy;
    public float EnergyAmount = 100;
    void Start()
    {
        energyManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoseEnergy(20);
        }
    }
    public void LoseEnergy (float loss)
    {
        EnergyAmount -= loss;
        RemainingEnergy.fillAmount = EnergyAmount / 100;
    }

    public void GainEnergy ( float healingAmount)
    {
        EnergyAmount += healingAmount;
        EnergyAmount = Mathf.Clamp(EnergyAmount, 0, 100);
        RemainingEnergy.fillAmount = EnergyAmount / 100;
    }
}
