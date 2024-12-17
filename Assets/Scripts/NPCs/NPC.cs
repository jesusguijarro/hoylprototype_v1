using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{
    [Header("Ink Json")]
    [SerializeField] private TextAsset[] inkJSONFiles;
    private int currentInkFileIndex = 0;

    [SerializeField] private GameObject admirationSign;

    public override void Interact()
    {
        Debug.Log("inkJSONFiles.Length: " + inkJSONFiles.Length);
        Debug.Log("Interacted");
        //DialogueSystem.Instance != null && 
        if (!DialogueSystem.Instance.dialogueIsPlaying)
        {
            if (currentInkFileIndex < inkJSONFiles.Length) 
            {                           
                DialogueSystem.Instance.EnterDialogueMode(inkJSONFiles[currentInkFileIndex]);
                currentInkFileIndex++;
                Debug.Log("currentInkFileIndex: " + currentInkFileIndex);
                if (currentInkFileIndex > inkJSONFiles.Length - 1) {
                    if (admirationSign) Destroy(admirationSign);
                    Debug.Log("destruír----------------------");
                }                    
            }
            else 
            {
                // DialogueSystem.Instance.EnterDialogueMode(inkJSONFiles[currentInkFileIndex-1]);
                Debug.Log("No more dialogues for this NPC.");
                //currentInkFileIndex++;
                Debug.Log("currentInkFileIndex: " + currentInkFileIndex);                
            }
        }
        //Debug.Log(inkJSON.text);
    }
}
