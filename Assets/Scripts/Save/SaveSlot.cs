using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct PlayerSaveData
{
    public Vector3 position;
    public float energy;
}

[Serializable]
public struct WorldSaveData
{
    public int currentDay;
}

[Serializable]
public struct ItemSaveData
{
    public string id;
    public int quantity;
}

[Serializable]
public struct InventorySaveData
{
    public ItemSaveData[] items;
}

[Serializable]
public struct DialogueSaveData
{
    public List<string> seenScripts;
    public string scriptId;
    public string dialogueId;
}

[Serializable]
public struct QuestSaveData
{
    public List<string> completed;
}

[Serializable]
public struct SaveSlot
{
    [HideLabel]
    public PlayerSaveData playerSaveData;
    [HideLabel] 
    public InventorySaveData inventorySaveData;
    [HideLabel]
    public WorldSaveData worldSaveData;
    [HideLabel]
    public DialogueSaveData dialogueSaveData;
    [HideLabel]
    public QuestSaveData questData;
}