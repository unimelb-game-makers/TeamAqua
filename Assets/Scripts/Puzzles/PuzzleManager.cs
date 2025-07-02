using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PuzzleManager : MonoBehaviour, ISaveable
{
    public static PuzzleManager instance;
    private Dictionary<string, PuzzleSaveData> _saveData = new Dictionary<string, PuzzleSaveData>();
    private List<Puzzle> _puzzles = new();
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Register(Puzzle puzzle)
    {
        _puzzles.Add(puzzle);
    }
    
    public void Load(SaveSlot saveSlot)
    {
        foreach (PuzzleSaveData saveData in saveSlot.worldSaveData.puzzles)
        {
            _saveData.Add(saveData.id, saveData);
        }
    }

    public bool TryGetSaveData(string id, out PuzzleSaveData puzzleSaveData)
    {
        return _saveData.TryGetValue(id, out puzzleSaveData);
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        PuzzleSaveData[] saveData = new PuzzleSaveData[_puzzles.Count];
        
        int index = 0;
        foreach (Puzzle puzzle in _puzzles)
        {
            saveData[index] = puzzle.GetSaveData();
            index++;
        }
        save.worldSaveData.puzzles = saveData;
        return save;
    }
}
