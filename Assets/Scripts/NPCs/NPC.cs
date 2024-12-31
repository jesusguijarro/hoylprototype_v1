using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [Header("Ink Json")]
    [SerializeField] private TextAsset[] inkJSONFiles;
    private int currentInkFileIndex = 0;

    [SerializeField] private GameObject admirationSign;

    [Header("Box Collider")]
    [SerializeField] private BoxCollider boxCollider;
    [Header("Box Collider")]
    [SerializeField] private BoxCollider boxColliderPortal;// Referencia al BoxCollider

    public override void Interact()
    {
        Debug.Log("inkJSONFiles.Length: " + inkJSONFiles.Length);
        Debug.Log("Interacted");

        if (!DialogueSystem.Instance.dialogueIsPlaying)
        {
            if (currentInkFileIndex < inkJSONFiles.Length)
            {
                DialogueSystem.Instance.EnterDialogueMode(inkJSONFiles[currentInkFileIndex]);
                currentInkFileIndex++;
                Debug.Log("currentInkFileIndex: " + currentInkFileIndex);

                if (currentInkFileIndex > inkJSONFiles.Length - 1)
                {
                    if (admirationSign)
                    {
                        Destroy(admirationSign);
                    }

                    // Verificar si el BoxCollider está asignado
                    if (boxCollider != null)
                    {
                        boxCollider.isTrigger = true; // Activar el trigger del BoxCollider
                        boxColliderPortal.enabled = true;
                        Debug.Log("BoxCollider activado.");
                    }
                    else
                    {
                        Debug.Log("BoxCollider no está asignado, no se realizó ninguna acción.");
                    }
                    if (boxColliderPortal != null)
                    {                      
                        boxColliderPortal.enabled = true;
                        Debug.Log("BoxCollider activado.");
                    }
                    else
                    {
                        Debug.Log("BoxCollider no está asignado, no se realizó ninguna acción.");
                    }
                }
            }
            else
            {
                Debug.Log("No more dialogues for this NPC.");
                Debug.Log("currentInkFileIndex: " + currentInkFileIndex);
            }
        }
    }
}
