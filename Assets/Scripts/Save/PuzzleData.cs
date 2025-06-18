using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A strictly developer-only scriptable object that should be used to auto-complete puzzles
/// </summary>
[CreateAssetMenu(fileName = "Puzzle Data", menuName = "ScriptableObjects/Puzzle Data")]
public class PuzzleData : ScriptableObject
{
    [ShowInInspector, ReadOnly]
    public PuzzleSaveData saveData;
    
#if UNITY_EDITOR
    /// <summary>
    /// This can be used in game, but you still need to save using CTRL+S
    /// </summary>
    [InfoBox("Then press to save the current status of the target puzzle." )]
    [Button]
    private void SnapshotPuzzleStatus()
    {
        Puzzle[] puzzles = FindObjectsByType<Puzzle>(FindObjectsSortMode.None);
        foreach (Puzzle puzzle in puzzles)
        {
            if (puzzle.ID == name)
            {
                saveData = puzzle.GetSaveData();
            }
        }
    }
#endif
}
