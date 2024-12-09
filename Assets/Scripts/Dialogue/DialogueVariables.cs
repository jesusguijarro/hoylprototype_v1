using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;
using UnityEngine.Networking;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables {  get; set; }
    public DialogueVariables(string globalsFilePath) 
    { 
        // compile the story
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }
    public void StartListening(Story story)
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }
    public void StopListening(Story story) 
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + name + " = " + value);
        // only maintain variables that were initialized from the globals ink file
        string questionString = name.Substring(1, 2);
        int qNumber = int.Parse(questionString);

        // Try to convert Ink.Runtime.Object to int
        int answerValue = 0;
        if (value is Ink.Runtime.IntValue intValue)
        {
            answerValue = intValue.value;
            Debug.Log("Converted value to int: " + answerValue);
        }
        else
        {
            Debug.LogError("Value is not an integer: " + value);
        }

        string username = PlayerPrefs.GetString("PlayerUsername");

        Debug.Log("qNumber " + qNumber);
        Debug.Log("answerValue " + answerValue);
        Debug.Log("username" + username);


        AnswerManager.Instance.SendAnswerToServer(qNumber, answerValue, username);
        // StartCoroutine(PostSaveAnswer(qNumber, answerValue, username));

        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
