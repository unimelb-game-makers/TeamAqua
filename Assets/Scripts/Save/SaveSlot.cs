using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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
public struct DialogueData
{
    public List<string> seen;
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
    public DialogueData dialogueSaveData;
    [HideLabel]
    public QuestSaveData questData;
}