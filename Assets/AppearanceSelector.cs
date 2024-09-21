using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceSelector : MonoBehaviour
{
    [Header("------ Buttons ------")]
    public Button maleButton;
    public Button femaleButton;
    [Header("------ Images ------")]
    public Image maleButtonImage;
    public Image femaleButtonImage;

    [Header("------ Colors ------")]
    public Color normalColor;
    public Color selectedColor;

    [Header("------ Selected Option ------")]
    public string selectedAppearance = "";

    public static AppearanceSelector instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        maleButton = GameObject.Find("MaleButton").GetComponent<Button>();
        femaleButton = GameObject.Find("FemaleButton").GetComponent<Button>();
        maleButtonImage = maleButton.GetComponent<Image>();
        femaleButtonImage = femaleButton.GetComponent<Image>();
        maleButton.onClick.AddListener(SelectMale);   
        femaleButton.onClick.AddListener(SelectFemale);
        //maleButton.Select();
    }

    private void SelectMale()
    {
        selectedAppearance = "MALE";        
        ColorBlock maleColorblock = maleButton.colors;
        maleColorblock.normalColor = selectedColor;
        maleButton.colors = maleColorblock;

        ColorBlock femaleColorBlock = femaleButton.colors;
        femaleColorBlock.normalColor = normalColor;
        femaleButton.colors = femaleColorBlock;
    }

    private void SelectFemale()
    {
        selectedAppearance = "FEMALE";
        ColorBlock maleColorBlock = maleButton.colors;
        maleColorBlock.normalColor = normalColor;
        maleButton.colors = maleColorBlock;

        ColorBlock femaleColorBlock = femaleButton.colors;
        femaleColorBlock.normalColor = selectedColor;
        femaleButton.colors = femaleColorBlock;
    }
}
