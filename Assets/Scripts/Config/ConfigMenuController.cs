using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMenuController : MonoBehaviour
{
    public GameObject configMenuPanel;
    
    void Update()
    {        
    }
    public void Config()
    {
        Debug.Log("Show Config Menu");
        configMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Hide Config Menu");
        configMenuPanel.SetActive(false);
    }
}
