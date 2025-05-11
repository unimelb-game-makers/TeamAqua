using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    public static readonly string[] SAVE_FILES = { "SAVE_SLOT_1", "SAVE_SLOT_2" };
    public const string SAVE_KEY = "SAVE_SLOT";
    private const string DEVELOP = "develop";

    [SerializeField] private SaveTemplate startSave;
    [SerializeField] private bool overrideSaveData;

    [ShowIf("overrideSaveData"), SerializeField] private SaveTemplate saveTemplate;

    [NonSerialized, ShowInInspector, ReadOnly] private string _saveSlotName = string.Empty;
    [NonSerialized, ShowInInspector, ReadOnly] private SaveSlot _saveSlot = new();


    private static readonly string SavePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" +
                                       Path.AltDirectorySeparatorChar;

    public void StartNewGame()
    {
        if (!HasEmptySave())
        {
            throw new InvalidOperationException("SAVE | Cannot start new game without an empty save slot");
        }

        for (int i = 0; i < SAVE_FILES.Length; ++i)
        {
            if (!HasSave(SAVE_FILES[i]))
            {
                SetSaveSlot(SAVE_FILES[i]);
                return;
            }
        }
    }

    public static bool HasEmptySave()
    {
        for (int i = 0; i < SAVE_FILES.Length; ++i)
        {
            if (!HasSave(SAVE_FILES[i]))
                return true;
        }

        return false;
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
            _saveSlot = saveTemplate.CreateSaveSlot();
            LoadSaveSlot();
            return;
        }
#endif

        string fullPath = GetFullPath(GetFileName());
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

    public SaveSlot GetSaveData(string fileName)
    {
        string path = GetFullPath(fileName);
        if (!File.Exists(path))
            throw new FileNotFoundException($"SAVE | Could not find a save file at {path}");
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveSlot>(json);
    }

    private void LoadSaveSlot()
    {
        List<MonoBehaviour> managers = Game.managers;
        foreach (MonoBehaviour manager in managers)
            if (manager.TryGetComponent(out ISaveable saveable))
                saveable.Load(_saveSlot);
    }

    public static bool HasSave(string fileName)
    {
        return File.Exists(GetFullPath(fileName));
    }
    
    private static string GetFullPath(string fileName)
    {
        return SavePath + fileName + ".json";
    }

    /// <summary>
    /// If there is no set save slot, it will just use develop.json
    /// </summary>
    /// <returns></returns>
    private string GetFileName()
    {
        return string.IsNullOrEmpty(_saveSlotName) ? DEVELOP : _saveSlotName;
    }
    

    [Button]
    public void Save()
    {
        List<MonoBehaviour> managers = Game.managers;
        foreach (MonoBehaviour manager in managers)
            if (manager.TryGetComponent(out ISaveable saveable))
                _saveSlot = saveable.Save(_saveSlot);

        string jsonString = JsonUtility.ToJson(_saveSlot);

        string fullPath = GetFullPath(GetFileName());
        using StreamWriter writer = new(fullPath);
        
        Debug.Log($"SAVE | Saving to {fullPath}");
        
        writer.Write(jsonString);
        PlayerPrefs.SetString(SAVE_KEY, GetFileName());
    }
}
