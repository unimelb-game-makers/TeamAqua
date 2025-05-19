using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    public static readonly string[] SAVE_FILES = { "SAVE_SLOT_1", "SAVE_SLOT_2", "SAVE_SLOT_3", "SAVE_SLOT_4", "SAVE_SLOT_5" };
    public const string SAVE_KEY = "SAVE_SLOT";
    public const string DEVELOP = "develop";

    public SaveTemplate startSave;
    public bool overrideSaveData;

    [ShowIf("overrideSaveData"), InlineEditor] public SaveTemplate saveTemplate;
    
    public static bool HasEmptySave()
    {
        for (int i = 0; i < SAVE_FILES.Length; ++i)
        {
            if (!HasSave(SAVE_FILES[i]))
                return true;
        }

        return false;
    }

    public static bool HasSave(string fileName)
    {
        return File.Exists(GetFullPath(fileName));
    }
    
    public static string GetFullPath(string fileName)
    {
        return GetSavePath() + fileName + ".json";
    }

    public static string GetSavePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves" +
               Path.AltDirectorySeparatorChar;
    }
}
