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
public struct DialogueNodeSaveData
{
    public string scriptId;
    public string dialogueId;
}

[Serializable]
public struct DialogueSaveData
{
    public DialogueNodeSaveData[] activeDialogues;
}

[Serializable]
public struct QuestStepSaveData
{
    public string id;
    public QuestState state;
}

[Serializable]
public struct QuestSaveData
{
    public string id;
    public QuestStepSaveData[] steps;
    public QuestState state;
}

[Serializable]
public struct JournalSaveData
{
    public QuestSaveData[] quests;
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
    public JournalSaveData journalSaveData;
}