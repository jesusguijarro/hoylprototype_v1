using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Button button;
    public Image targetGraphic;
    public Color normalColor;
    public Color selectedColor;
    private bool isSelected = false;
    void Start()
    {
        button.onClick.AddListener(ToggleSelection);   
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        targetGraphic.color = isSelected ? normalColor : selectedColor;
    }    
}
