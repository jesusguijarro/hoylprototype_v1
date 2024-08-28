using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();
    
    Button continueBtn;
    TMP_Text dialogueTxt, nameTxt;
    int dialogueIndex;
    void Awake()
    {
        continueBtn = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        dialogueTxt = dialoguePanel.transform.Find("Text").GetComponent<TMP_Text>();
        nameTxt = dialoguePanel.transform.Find("NPCName").GetChild(0).GetComponent<TMP_Text>();
        continueBtn.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want this to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines.Clear(); // Clears the list instead of creating a new one
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        
        CreateDialogue();
    }

    public void CreateDialogue() 
    { 
        dialogueTxt.text = dialogueLines[dialogueIndex];
        nameTxt.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue() 
    { 
        if(dialogueIndex < dialogueLines.Count-1)
        { 
            dialogueIndex++;
            dialogueTxt.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
