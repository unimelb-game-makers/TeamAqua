using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Script
{
    public string id;
    public TextAsset script;
    public List<string> dialogueIds;

    public string GetNextDialogue(string dialogueId)
    {
        int currentIndex = dialogueIds.IndexOf(dialogueId);
        
        if (currentIndex == -1)
            throw new ArgumentException($"DIALOGUE | Dialogue ID '{dialogueId}' not found in the list.");

        return currentIndex + 1 >= dialogueIds.Count ? string.Empty : dialogueIds[currentIndex + 1];
    }
}
