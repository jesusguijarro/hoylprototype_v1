using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    //private bool pauseMenuIsActive;

    private void Update()
    {
    }

    public void Pause()
    {
        Debug.Log("entré al pause");
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
