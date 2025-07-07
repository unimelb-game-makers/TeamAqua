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

    public Dictionary<string, string> GetEmptySave()
    {
        Dictionary<string, string> save = new();
        foreach (DialogueScript script in dialogueBranches)
            save.Add(script.name, script.GetFirstNode().name);
        return save;
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
