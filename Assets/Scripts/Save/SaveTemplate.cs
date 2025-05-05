using UnityEngine;

/// <summary>
/// A strictly developer-only scriptable object that should be set in replacement of a save slot.
/// </summary>
[CreateAssetMenu(fileName = "Save Template", menuName = "ScriptableObjects/Save Template")]
public class SaveTemplate : ScriptableObject
{
    [TextArea]
    public string description;
    public SaveSlot saveSlot = new();
}