using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Dialogue Pool", fileName = "Dialogue Pool")]
public class DialoguePool : ScriptableObject
{
    public DialogueScript startScript;

    [InlineEditor]
    public List<DialogueScript> dialogueBranches;

    public DialogueScript GetScript(string id)
    {
        for (int i = 0; i < dialogueBranches.Count; ++i)
        {
            if (dialogueBranches[i].name == id)
            {
                return dialogueBranches[i];
            }
        }

        throw new KeyNotFoundException($"DIALOGUE | Script '{id}' not found in the list.");
    }

    public DialogueScript GetNextScript(string scriptId)
    {
        DialogueScript dialogueScript = GetScript(scriptId);
        int index = dialogueBranches.IndexOf(dialogueScript);
        // We will return empty if there are no more dialogueBranches
        return index + 1 >= dialogueBranches.Count ? null : dialogueBranches[index + 1];
    }

    /// <summary>
    /// Checks whether the player has already seen a particular combination of script and nodes before.
    /// </summary>
    /// <param name="script"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool HasSeen(DialogueScript script, DialogueNode node)
    {
        // If we haven't started anything, then we haven't seen anything.
        if (string.IsNullOrEmpty(DialogueManager.instance.ScriptId))
            return false;

        DialogueScript currentScript = GetScript(DialogueManager.instance.ScriptId);
        script.TryGetDialogue(
            DialogueManager.instance.DialogueId,
            out DialogueNode currentDialogue
        );

        // Steven: no need to check script as we switch between them
        // // First compare the dialogueBranches
        // int currentScriptIndex = dialogueBranches.IndexOf(currentScript);
        // int scriptIndex = dialogueBranches.IndexOf(script);

        // compare dialogue nodes
        int currentDialogueIndex = currentDialogue
            ? currentScript.dialogues.IndexOf(currentDialogue)
            : 0;
        int dialogueIndex = node ? script.dialogues.IndexOf(node) : 0;

        // if (currentScriptIndex > scriptIndex)
        //     return true;
        Debug.Log(
            $"SEEN | current dialogue: {currentDialogue.name}|[{currentDialogueIndex}] and passed dialogue: {node.name}|[{dialogueIndex}]"
        );
        if (currentDialogueIndex > dialogueIndex)
            return true;
        return false;
    }

    public bool NodeValid(DialogueScript script, DialogueNode node)
    {
        /// current BUG: might be a save slot thingo but first noonisland dialogue skipped
        /// already tried deleting develop.js and checking save templates
        ///
        // valid if node is the script's active node
        if (script.activeNode == null)
        {
            Debug.Log("First node");
            script.activeNode = script.dialogues[0];
        }

        if (node == script.activeNode)
        {
            Debug.Log($"POOL | node [{node.name}] is valid");
            return true;
        }

        // false if the node has passed or not active yet
        Debug.Log($"POOL | node [{node.name}] is NOT valid");
        return false;
    }

    private void OnValidate()
    {
        List<string> scriptIds = new();
        List<string> dialogueIds = new();
        for (int i = 0; i < dialogueBranches.Count; ++i)
        {
            if (dialogueBranches[i] == null)
                continue;
            if (scriptIds.Contains(dialogueBranches[i].name))
                throw new InvalidOperationException(
                    $"DIALOGUE | Duplicate script name detected: '{dialogueBranches[i].name}'"
                );
            scriptIds.Add(dialogueBranches[i].name);
            for (int j = 0; j < dialogueBranches[i].dialogues.Count; ++j)
            {
                if (dialogueIds.Contains(dialogueBranches[i].dialogues[j].name))
                    throw new InvalidOperationException(
                        $"DIALOGUE | Duplicate dialogue name detected: '{dialogueBranches[i].dialogues[j].name}'"
                    );
                dialogueIds.Add(dialogueBranches[i].dialogues[j].name);
            }
        }
    }
}
