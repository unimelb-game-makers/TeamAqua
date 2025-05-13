using UnityEngine;

[CreateAssetMenu(menuName = "Creature System/Creature Database")]
public class CreatureDatabase : ScriptableObject
{
    public CreatureData[] creatures;

    public CreatureData GetCreatureDataByName(string name)
    {
        foreach (var creature in creatures)
        {
            if (creature.creatureName == name)
                return creature;
        }
        return null;
    }
}
