using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Day Scriptable", fileName = "Day")]
public class Day : ScriptableObject
{
    [InlineEditor]
    public DialoguePool dialoguePool;
<<<<<<< HEAD
    public List<Quest> mainQuests;
    public List<Quest> subQuests;
=======
    [InlineEditor] 
    public WorldDatabase worldDatabase;
    public List<Quest> mainQuests;
    public List<Quest> subQuests;

    public void Init()
    {
        worldDatabase.Init();
    }

    /// <summary>
    /// To be called when entering into a new day
    /// </summary>
    public void Enter(WorldData worldData)
    {
        worldDatabase.Enable(worldData);
    }

    /// <summary>
    /// To be called when existing the current day
    /// </summary>
    public void Exit(WorldData worldData)
    {
        worldDatabase.Disable(worldData);
    }
>>>>>>> main
}
