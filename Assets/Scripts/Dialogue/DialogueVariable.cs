using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class DialogueVariable
{
    public Dictionary<string, Ink.Runtime.Object> variables {get; private set; }
    public DialogueVariable(TextAsset LoadGlobalJSON)
    {       //create the story
        Story globalVariablesStory = new Story(LoadGlobalJSON.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variables: " + name + " = " + value);
        }
    }
    public void StartListening(Story story)
    {
        
        VariableToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //Debug.Log("Variable changed: " + name + " = " + value);
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariableToStory(Story story)
    {
        //only maintain variables that were initialized from the globals ink file
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
