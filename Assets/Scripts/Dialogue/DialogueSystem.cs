using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private bool canSkip = false;
    private bool submitSkip = false;
    
    private Coroutine displayLineCoroutine;

    //private bool canContinueToNextLine = false; // new
    private bool isContinueButtonPressed = false; // new    
    Button continueBtn;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private DialogueVariables dialogueVariables;

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

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            submitSkip = true;
        }

        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) {
            return;        
        }
        // handle continuing to the next line in the dialogue when submit is pressed
        if ( canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && isContinueButtonPressed)
        {
            ContinueStory();
            isContinueButtonPressed = false;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        string playerName = PlayerPrefs.GetString("PlayerUsername");
        string inkText = inkJSON.text.Replace("~PlayerUsername", playerName);

        // Procesar el contenido del archivo Ink
        inkText = ProcessInkContent(inkText);

        currentStory = new Story(inkText);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        //currentStory.BindExternalFunction("endGame", () =>
        //{
        //    Debug.Log("Fin del juego.");
        //});

        // Resetear etiquetas iniciales
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();
    }


    private IEnumerable ExitDialogueMode()
    {
        yield return new WaitForSeconds(5f);
        
        dialogueVariables.StopListening(currentStory);
        //currentStory.UnbindExternalFunction("endGame");
        
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
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            //dialogueText.text = currentStory.Continue();        old line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        dialogueText.text = "";

        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        submitSkip = false;
        canContinueToNextLine = false;

        StartCoroutine(CanSkip());

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (canSkip && submitSkip)
            {
                submitSkip = false;
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private IEnumerator CanSkip()
    {
        canSkip = false; //Making sure the variable is false.
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // Recorre cada etiqueta y manéjala según corresponda
        foreach (string tag in currentTags)
        {
            // Divide la etiqueta
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
                continue;
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Maneja la etiqueta
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;

                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;

                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;

                default:
                    Debug.LogWarning("Tag came in but is not currently being handled");
                    break;
            }
        }
    }
    public string ProcessInkContent(string inkContent)
    {
        string appearance = PlayerPrefs.GetString("PlayerAppearance", "MALE").ToLower(); // Default to "male"

        // Sustituir etiquetas específicas según la apariencia
        inkContent = inkContent.Replace("player_neutral", $"{appearance}_player_neutral");
        inkContent = inkContent.Replace("player_happy", $"{appearance}_player_happy");
        inkContent = inkContent.Replace("player_sad", $"{appearance}_player_sad");

        return inkContent;
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
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }        
    }

    public Ink.Runtime.Object GetVariableState(string variableName) 
    { 
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableName == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName); 
        }
        return variableValue;
    }

}
