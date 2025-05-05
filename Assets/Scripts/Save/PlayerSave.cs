using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
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
    public void Load(string saveSlot)
    {
#if UNITY_EDITOR
        if (overrideSaveData)
        {
            _saveSlot = saveTemplate.saveSlot;
            return;
        }
#endif

        string fullPath = SavePath + saveSlot;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            _saveSlot = JsonUtility.FromJson<SaveSlot>(json);
        }
        else
        {
            Debug.Log($"Save file not found at {fullPath}. Creating empty save slot.");
            _saveSlot = new SaveSlot(); 
        }
    }

    [Button]
    public void Save()
    {
        foreach (ISaveable saveable in saveables)
        {
            _saveSlot = saveable.Save(_saveSlot);
        }

        string jsonString = JsonUtility.ToJson(_saveSlot);
        Debug.Log(jsonString);

        using StreamWriter writer =
            new (Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json");
        writer.Write(jsonString);
    }
}
