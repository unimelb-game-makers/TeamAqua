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
    public List<Quest> mainQuests;
    public List<Quest> subQuests;
}
