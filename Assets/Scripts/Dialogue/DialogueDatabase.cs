using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue Database", fileName = "Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public string startScriptId;
    
    public List<Script> scripts;

    public bool TryGetScript(string id, out Script script)
    {
        for (int i = 0; i < scripts.Count; ++i)
        {
            if (scripts[i].id == id)
            {
                script = scripts[i];
                return true;
            }
        }

        script = null;
        return false;
    }
    
    public string GetNextScript(string scriptId)
    {
        int index = scripts.Count;
        for (int i = 0; i < scripts.Count; ++i)
        {
            if (scripts[i].id == scriptId)
            {
                index = i;
                break;
            }
        }
        
        // We will return empty if there are no more scripts
        return index + 1 >= scripts.Count ? string.Empty : scripts[index + 1].id;
    }
}