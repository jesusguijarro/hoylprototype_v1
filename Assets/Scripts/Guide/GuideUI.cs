using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuideUIManager : MonoBehaviour
{
    public static GuideUIManager Instance { get; private set; }

    public GameObject guidePanel;
    public TextMeshProUGUI titleGuide, descriptionGuide;
    public Image imageGuide;
    public Button continueButton;
    public GameObject endPanel;

    private void Awake()
    {
        // Ensure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if necessary
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //gameObject.SetActive(true);        
        guidePanel.SetActive(false);
        endPanel.SetActive(false);
        continueButton.onClick.AddListener(Disable);
    }
    public void Parameters(string title, string description, Sprite image)
    {        
        titleGuide.text = title;
        descriptionGuide.text = description;
        imageGuide.sprite = image;
        //gameObject.SetActive(true);
        guidePanel.SetActive(true);
    }    
    public void Disable()
    {
        //gameObject.SetActive(false);
        guidePanel.SetActive(false);
    }

    public void EnableEndPanel()
    {
        endPanel.SetActive(true);
        ResetGame();
    }

    public void ResetGame()
    {
        // after the end panel we reset the game
        Debug.Log("You already ended the game! We are reseting all the values for a new game.");
        PlayerPrefs.DeleteAll();
    }
}
