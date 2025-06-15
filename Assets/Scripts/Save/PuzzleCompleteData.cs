using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A strictly developer-only scriptable object that should be used to auto-complete puzzles
/// </summary>
[CreateAssetMenu(fileName = "Puzzle Complete Data", menuName = "ScriptableObjects/Puzzle Complete Data")]
public class PuzzleCompleteData : ScriptableObject
{
    [InfoBox("Set this to the target puzzle. Then press SnapshotPuzzleStatus to save the current status of the target puzzle." )]
    public string id;

    [ShowInInspector, ReadOnly]
    public PuzzleSaveData saveData;
    
#if UNITY_EDITOR
    /// <summary>
    /// This can be used in game, but you still need to save using CTRL+S
    /// </summary>
    [Button]
    private void SnapshotPuzzleStatus()
    {
        Puzzle[] puzzles = FindObjectsByType<Puzzle>(FindObjectsSortMode.None);
        foreach (Puzzle puzzle in puzzles)
        {
            if (puzzle.id == id)
            {
                saveData = puzzle.GetSaveData();
            }
        }
    }
#endif
}
