using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "ScriptableObjects/NpcData", order = 0)]
public class NpcData : ScriptableObject
{
    public List<DialogueTrigger> dialogues;
}
