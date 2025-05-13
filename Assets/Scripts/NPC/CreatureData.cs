using UnityEngine;

[CreateAssetMenu(menuName = "Creature System/Creature Data")]
public class CreatureData : ScriptableObject
{
    public string creatureName;
    public CollectionData[] collectionData;

    public Creature Create()
    {
        return new Creature(creatureName);
    }
}