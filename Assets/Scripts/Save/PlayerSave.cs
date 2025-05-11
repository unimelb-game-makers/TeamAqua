using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    private const string DEVELOP = "develop";

    [SerializeField] private SaveTemplate startSave;
    [SerializeField] private bool overrideSaveData;

    [ShowIf("overrideSaveData"), SerializeField] private SaveTemplate saveTemplate;

    private string _saveSlotName = string.Empty;
    [NonSerialized, ShowInInspector, ReadOnly] private SaveSlot _saveSlot = new();


    private readonly string _savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" +
                                       Path.AltDirectorySeparatorChar;
    
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
            _saveSlot = saveTemplate.CreateSaveSlot();
            LoadSaveSlot();
            return;
        }
#endif

        string fullPath = GetFullPath();
        if (File.Exists(fullPath))
        {
            Debug.Log($"SAVE | Save file found at {fullPath}.");
            string json = File.ReadAllText(fullPath);
            _saveSlot = JsonUtility.FromJson<SaveSlot>(json);
        }
        else
        {
            Debug.Log($"SAVE | Save file not found at {fullPath}. Creating empty save slot.");
            _saveSlot = startSave.CreateSaveSlot();
        }

        LoadSaveSlot();
    }

    private void LoadSaveSlot()
    {
        List<MonoBehaviour> managers = Game.managers;
        foreach (MonoBehaviour manager in managers)
            if (manager.TryGetComponent(out ISaveable saveable))
                saveable.Load(_saveSlot);
    }

    private string GetFullPath()
    {
        return _savePath + GetFileName();
    }

    /// <summary>
    /// If there is no set save slot, it will just use develop.json
    /// </summary>
    /// <returns></returns>
    private string GetFileName()
    {
        string file = string.IsNullOrEmpty(_saveSlotName) ? DEVELOP : _saveSlotName;
        return file + ".json";
    }

    [Button]
    public void Save()
    {
        List<MonoBehaviour> managers = Game.managers;
        foreach (MonoBehaviour manager in managers)
            if (manager.TryGetComponent(out ISaveable saveable))
                _saveSlot = saveable.Save(_saveSlot);

        string jsonString = JsonUtility.ToJson(_saveSlot);

        string fullPath = GetFullPath();
        using StreamWriter writer = new(fullPath);
        
        Debug.Log($"SAVE | Saving to {fullPath}");
        
        writer.Write(jsonString);
    }
}
