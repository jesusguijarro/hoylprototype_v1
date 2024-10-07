using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{
    [Header("Ink Json")]
    [SerializeField] private TextAsset[] inkJSONFiles;
    private int currentInkFileIndex = 0;

    public override void Interact()
    {
        Debug.Log("Interacted");
        //DialogueSystem.Instance != null && 
        if (!DialogueSystem.Instance.dialogueIsPlaying)
        {
            if (currentInkFileIndex < inkJSONFiles.Length) 
            {
                DialogueSystem.Instance.EnterDialogueMode(inkJSONFiles[currentInkFileIndex]);
                currentInkFileIndex++;
            }
            else 
            {
                Debug.Log("No more dialogues for this NPC.");
            }
        }
        //Debug.Log(inkJSON.text);
    }
}
