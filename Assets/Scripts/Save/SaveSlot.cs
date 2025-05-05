using System;
using System.Collections.Generic;
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
    public string id;
    public PlayerSaveData playerSaveData;
    public WorldSaveData worldSaveData;
    public DialogueData dialogueSaveData;
    public QuestSaveData questData;
}