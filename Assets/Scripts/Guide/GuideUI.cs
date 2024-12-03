using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuideUI : MonoBehaviour
{
    TextMeshProUGUI titleGuide, descriptionGuide;
    Image imageGuide;
    Button continueButton;

    void Start()
    {
        titleGuide = transform.Find("Title_Guide").GetComponent<TextMeshProUGUI>();
        descriptionGuide = transform.Find("Description_Guide").GetComponent <TextMeshProUGUI>();
        imageGuide = transform.Find("Image_Guide").GetComponent<Image>();
        continueButton = transform.Find("Button").GetComponent<Button>();
        gameObject.SetActive(false);
    }

    public void Enable(string title, string description, Sprite image)
    {
        titleGuide.text = title;
        descriptionGuide.text = description;
        imageGuide.sprite = image;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
