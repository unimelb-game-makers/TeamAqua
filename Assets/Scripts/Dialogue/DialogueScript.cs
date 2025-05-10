using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Script", fileName = "Script")]
public class DialogueScript : ScriptableObject
{
    public TextAsset inkFile;
    public List<DialogueNode> dialogues;

    /// <summary>
    /// No need to throw an error here, assume that dialogues can be falsy
    /// </summary>
    /// <param name="dialogueId"></param>
    /// <param name="dialogueNode"></param>
    /// <returns></returns>
    public bool TryGetDialogue(string dialogueId, out DialogueNode dialogueNode)
    {
        for (int i = 0; i < dialogues.Count; ++i)
        {
            if (dialogues[i].name == dialogueId)
            {
                dialogueNode = dialogues[i];
                return true;
            }
        }

        dialogueNode = null;
        return false;
    }
    

    public string GetNextDialogue(string dialogueId)
    {
        if (!TryGetDialogue(dialogueId, out DialogueNode dialogue))
        {
            return string.Empty;
        }

        if (dialogue.infinite)
            return dialogue.name;
        int index = dialogues.IndexOf(dialogue);
        return index + 1 >= dialogues.Count ? string.Empty : dialogues[index + 1].name;
    }
}
