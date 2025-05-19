using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue Database", fileName = "Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public DialogueScript startScript;

    [InlineEditor]
    public List<DialogueScript> scripts;

    public DialogueScript GetScript(string id)
    {
        for (int i = 0; i < scripts.Count; ++i)
        {
            if (scripts[i].name == id)
            {
                return scripts[i];
            }
        }

        throw new KeyNotFoundException($"DIALOGUE | Script '{id}' not found in the list.");
    }

    public DialogueScript GetNextScript(string scriptId)
    {
        DialogueScript dialogueScript = GetScript(scriptId);
        int index = scripts.IndexOf(dialogueScript);
        // We will return empty if there are no more scripts
        return index + 1 >= scripts.Count ? null : scripts[index + 1];
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
        // First compare the scripts
        int currentScriptIndex = scripts.IndexOf(currentScript);
        int scriptIndex = scripts.IndexOf(script);
        int currentDialogueIndex = currentDialogue
            ? currentScript.dialogues.IndexOf(currentDialogue)
            : 0;
        int dialogueIndex = node ? script.dialogues.IndexOf(node) : 0;
        if (currentScriptIndex > scriptIndex)
            return true;
        if (currentDialogueIndex > dialogueIndex)
            return true;
        return false;
    }

    private void OnValidate()
    {
        List<string> scriptIds = new();
        List<string> dialogueIds = new();
        for (int i = 0; i < scripts.Count; ++i)
        {
            if (scripts[i] == null)
                continue;
            if (scriptIds.Contains(scripts[i].name))
                throw new InvalidOperationException(
                    $"DIALOGUE | Duplicate script name detected: '{scripts[i].name}'"
                );
            scriptIds.Add(scripts[i].name);
            for (int j = 0; j < scripts[i].dialogues.Count; ++j)
            {
                if (dialogueIds.Contains(scripts[i].dialogues[j].name))
                    throw new InvalidOperationException(
                        $"DIALOGUE | Duplicate dialogue name detected: '{scripts[i].dialogues[j].name}'"
                    );
                dialogueIds.Add(scripts[i].dialogues[j].name);
            }
        }
    }
}
