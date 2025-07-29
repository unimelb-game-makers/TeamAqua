using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/World Database", fileName = "World Database")]
public class WorldDatabase : ScriptableObject
{
    public List<ID> npcIds = new();

    private HashSet<ID> _ids = new();

    public void Init()
    {
        foreach (ID npc in npcIds) _ids.Add(npc);
    }

    public bool CanEnable(ID id)
    {
        return _ids.Contains(id);
    }

    /// <summary>
    /// Turns on all of the NPCs in the ID list
    /// </summary>
    /// <param name="worldData"></param>
    public void Enable(WorldData worldData)
    {
        foreach (ID id in _ids)
        {
            if (worldData.npcs.TryGetValue(id, out NPC npc))
                npc.gameObject.SetActiveFast(true);
        }
    }
    
    /// <summary>
    /// Turns off all of the NPCs in the ID list
    /// </summary>
    /// <param name="worldData"></param>
    public void Disable(WorldData worldData)
    {
        foreach (ID id in _ids)
        {
            if (worldData.npcs.TryGetValue(id, out NPC npc))
                npc.gameObject.SetActiveFast(false);
        }
    }
}
