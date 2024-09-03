using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    //private bool canContinueToNextLine = false; // new
    private bool isContinueButtonPressed = false; // new
    
    Button continueBtn;
    void Awake()
    {
        continueBtn = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        continueBtn.onClick.AddListener(delegate { ContinueDialogue(); });       

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want this to persist across scenes
            Debug.Log("DialogueSystem instance created");
        }
        else
        {
            Debug.LogWarning("Duplicate DialogueSystem instance detected and destroyed in scene: " +
                gameObject.scene.name + ", GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) {
            return;        
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        if (isContinueButtonPressed)
        {
            ContinueStory();
            isContinueButtonPressed = false;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";        
    }

    public void ContinueDialogue() 
    {
        isContinueButtonPressed = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {            
            dialogueText.text = currentStory.Continue();                                    
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
