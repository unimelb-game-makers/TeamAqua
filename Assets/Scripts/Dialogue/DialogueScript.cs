using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Script", fileName = "Script")]
public class DialogueScript : ScriptableObject
{
    public TextAsset inkFile;
    public List<DialogueNode> dialogues;

    // each script keeps a currentNode, next time a node in this script is called, it should be this node

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

    public DialogueNode GetNextDialogue(string dialogueId)
    {
        if (!TryGetDialogue(dialogueId, out DialogueNode dialogue))
        {
            return null;
        }

        if (dialogue.infinite)
            return dialogue;
        int index = dialogues.IndexOf(dialogue);
        return index + 1 >= dialogues.Count ? null : dialogues[index + 1];
    }

    public DialogueNode GetFirstNode()
    {
        if (dialogues.Count == 0)
        {
            throw new IndexOutOfRangeException("This is an empty dialogue script");
        }

        return dialogues[0];
    }
}
