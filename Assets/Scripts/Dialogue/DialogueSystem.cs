using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

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

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) {
            return;        
        }
        // handle continuing to the next line in the dialogue when submit is pressed
        if (currentStory.currentChoices.Count == 0 && isContinueButtonPressed)
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
            // display choices, if any, for this dialogue line
            DisplayChoices(); //*****************************
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Numver of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);        
        ContinueStory();
    }

}
