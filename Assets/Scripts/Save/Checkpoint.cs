using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A strictly developer-only scriptable object that should be set in replacement of a save slot.
/// </summary>
[CreateAssetMenu(fileName = "Checkpoint", menuName = "ScriptableObjects/Checkpoint")]
public class Checkpoint : ScriptableObject
{
    public Vector3 position;

#if UNITY_EDITOR
    /// <summary>
    /// This can be used in game, but you still need to save using CTRL+S
    /// </summary>
    [Button]
    private void SnapshotPlayerPosition()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        position = playerController.transform.position;
    }
#endif
}