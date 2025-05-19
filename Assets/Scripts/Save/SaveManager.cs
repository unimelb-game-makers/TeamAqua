using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [SerializeField] private PlayerSave playerSave;
    
    [NonSerialized, ShowInInspector, ReadOnly] private string _saveSlotName = string.Empty;
    [NonSerialized, ShowInInspector, ReadOnly] private SaveSlot _saveSlot = new();

    private void Awake()
    {
       if (instance != null && instance != this)
           Destroy(gameObject);
       else
           instance = this;
       DontDestroyOnLoad(gameObject);
    }
    
    public SaveSlot GetSaveData(string fileName)
    {
        string path = PlayerSave.GetFullPath(fileName);
        if (!File.Exists(path))
            throw new FileNotFoundException($"SAVE | Could not find a save file at {path}");
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveSlot>(json);
    }
    
    public void StartNewGame()
    {
        if (!PlayerSave.HasEmptySave())
        {
            throw new InvalidOperationException("SAVE | Cannot start new game without an empty save slot");
        }

        for (int i = 0; i < PlayerSave.SAVE_FILES.Length; ++i)
        {
            if (!PlayerSave.HasSave(PlayerSave.SAVE_FILES[i]))
            {
                SetSaveSlot(PlayerSave.SAVE_FILES[i]);
                return;
            }
        }
    }
    
    /// <summary>
    /// Run at the start of the game. It will load up any values from the save slot.
    /// If the save file does not exist, it creates a new empty save slot.
    /// </summary>
    public void Load()
    {
#if UNITY_EDITOR
        if (playerSave.overrideSaveData)
        {
            _saveSlot = playerSave.saveTemplate.CreateSaveSlot();
            LoadSaveSlot();
            return;
        }
#endif

        string fullPath = PlayerSave.GetFullPath(GetFileName());
        if (File.Exists(fullPath))
        {
            Debug.Log($"SAVE | Save file found at {fullPath}.");
            string json = File.ReadAllText(fullPath);
            _saveSlot = JsonUtility.FromJson<SaveSlot>(json);
        }
        else
        {
            Debug.Log($"SAVE | Save file not found at {fullPath}. Creating empty save slot.");
            _saveSlot = playerSave.startSave.CreateSaveSlot();
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
    
    
    public void SetSaveSlot(string saveSlot)
    {
        _saveSlotName = saveSlot;
    }
    
    /// <summary>
    /// If there is no set save slot, it will just use develop.json
    /// </summary>
    /// <returns></returns>
    private string GetFileName()
    {
        return string.IsNullOrEmpty(_saveSlotName) ? PlayerSave.DEVELOP : _saveSlotName;
    }
    
    [Button]
    public void Save()
    {
        List<MonoBehaviour> managers = Game.managers;
        foreach (MonoBehaviour manager in managers)
            if (manager.TryGetComponent(out ISaveable saveable))
                _saveSlot = saveable.Save(_saveSlot);

        string jsonString = JsonUtility.ToJson(_saveSlot);

        Directory.CreateDirectory(PlayerSave.GetSavePath());
        string fullPath = PlayerSave.GetFullPath(GetFileName());
        using StreamWriter writer = new(fullPath);
        
        Debug.Log($"SAVE | Saving to {fullPath}");
        
        writer.Write(jsonString);
        PlayerPrefs.SetString(PlayerSave.SAVE_KEY, GetFileName());
    }
}