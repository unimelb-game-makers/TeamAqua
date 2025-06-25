using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Day Scriptable", fileName = "Day")]
public class Day : ScriptableObject
{
    public DialoguePool dialoguePool;
    public List<Quest> mainQuests;
    public List<Quest> SubQuests;
}
