using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "ScriptableObjects/NpcData", order = 0)]
public class NpcData : ScriptableObject
{
    //MIGHT MERGE WITH NPC.cs


    public string questID;
    public bool HasQuest;
}
