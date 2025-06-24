using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "ScriptableObjects/Dialogue/new/Dialogue Pool",
    fileName = "Dialogue Pool"
)]
public class DialoguePool : ScriptableObject
{
    public TextAsset inkFile;
    public List<DialogueNode> dialogues;
}
