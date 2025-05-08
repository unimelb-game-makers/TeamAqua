using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue Database", fileName = "Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public string startScriptId;
    public string startDialogueId;
    
    [TableList]
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
}