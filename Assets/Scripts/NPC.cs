using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJSON;

    public override void Interact()
    {
        //DialogueSystem.Instance != null && 
        if (!DialogueSystem.Instance.dialogueIsPlaying)
        {
            DialogueSystem.Instance.EnterDialogueMode(inkJSON);
        }
        Debug.Log(inkJSON.text);
    }
}
