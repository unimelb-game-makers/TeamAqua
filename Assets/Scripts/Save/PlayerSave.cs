using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    [SerializeField] private bool overrideSaveData;

    [ShowIf("overrideSaveData"), SerializeField] private SaveTemplate saveTemplate;

    [NonSerialized, ShowInInspector, ReadOnly] private SaveSlot _saveSlot = new();

    private List<ISaveable> saveables = new();

    public void Register(ISaveable saveable)
    {
        saveables.Add(saveable);
    }

    public void SetSaveSlot(SaveSlot saveSlot)
    {
        _saveSlot = saveSlot;
    }
    
    /// <summary>
    /// Run at the start of the game. It will load up any values from the save slot
    /// </summary>
    public void Load()
    {
#if UNITY_EDITOR
        if (overrideSaveData)
        {
            _saveSlot = saveTemplate.saveSlot;
        }
#endif
    }

    public void Save()
    {
        // This is gonna get hairy
        foreach (ISaveable saveable in saveables)
        {
            _saveSlot = saveable.Save(_saveSlot);
        }
    }
}
