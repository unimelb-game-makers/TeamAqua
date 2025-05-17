using System;
using UnityEngine;
using UnityEngine.Assertions;

public class EnergyManager : MonoBehaviour, ISaveable
{
    public static EnergyManager instance;
    public const float MAX_ENERGY = 100f;
    public static Action<float> OnEnergyChanged;
    [SerializeField] private PlayerSave playerSave;

    private float _energyAmount = 100;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Load(SaveSlot saveSlot)
    {
        _energyAmount = saveSlot.playerSaveData.energy;
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        save.playerSaveData.energy = _energyAmount;
        return save;
    }
    
    private void Start()
    {
        OnEnergyChanged?.Invoke(_energyAmount);
    }

#if UNITY_EDITOR
    // Debug logic
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoseEnergy(20);
        }
    }
    
#endif

    public void LoseEnergy(float loss)
    {
        if (_energyAmount == 0)
        {
            return;
        }

        _energyAmount = Math.Max(_energyAmount - loss, 0);
        OnEnergyChanged?.Invoke(_energyAmount);
    }

    public void OnNextDay()
    {
        _energyAmount = MAX_ENERGY;
        OnEnergyChanged?.Invoke(_energyAmount);
    }

    public void GainEnergy(float healingAmount)
    {
        _energyAmount += healingAmount;
        _energyAmount = Mathf.Clamp(_energyAmount, 0, MAX_ENERGY);
        OnEnergyChanged?.Invoke(_energyAmount);
    }
}
