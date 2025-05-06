using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    private const string DEVELOP = "develop";
    
    [SerializeField] private bool overrideSaveData;

    [ShowIf("overrideSaveData"), SerializeField] private SaveTemplate saveTemplate;

    private string _saveSlotName = string.Empty;
    [NonSerialized, ShowInInspector, ReadOnly] private SaveSlot _saveSlot = new();

    private List<ISaveable> saveables = new();

    private readonly string SavePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" +
                                       Path.AltDirectorySeparatorChar;
    
    public void Register(ISaveable saveable)
    {
        saveables.Add(saveable);
    }

    public void SetSaveSlot(string saveSlot)
    {
        _saveSlotName = saveSlot;
    }
    
    /// <summary>
    /// Run at the start of the game. It will load up any values from the save slot.
    /// If the save file does not exist, it creates a new empty save slot.
    /// </summary>
    public void Load()
    {
#if UNITY_EDITOR
        if (overrideSaveData)
        {
            _saveSlot = saveTemplate.saveSlot;
            return;
        }
#endif

        string fullPath = SavePath + GetFileName();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            _saveSlot = JsonUtility.FromJson<SaveSlot>(json);
        }
        else
        {
            Debug.Log($"SAVE | Save file not found at {fullPath}. Creating empty save slot.");
            _saveSlot = new SaveSlot(); 
        }
    }

    /// <summary>
    /// If there is no set save slot, it will just use develop.json
    /// </summary>
    /// <returns></returns>
    private string GetFileName()
    {
        return string.IsNullOrEmpty(_saveSlotName) ? DEVELOP : _saveSlotName + ".json";
    }

    [Button]
    public void Save()
    {
        foreach (ISaveable saveable in saveables)
        {
            _saveSlot = saveable.Save(_saveSlot);
        }

        string jsonString = JsonUtility.ToJson(_saveSlot);

        using StreamWriter writer =
            new (Application.dataPath + Path.AltDirectorySeparatorChar + GetFileName());
        
        Debug.Log($"SAVE | Saving to {GetFileName()}");
        
        writer.Write(jsonString);
    }
}
