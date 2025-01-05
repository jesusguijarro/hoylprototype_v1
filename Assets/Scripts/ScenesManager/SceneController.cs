using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        int passToGame = PlayerPrefs.GetInt("PassToGame");
        if (PlayerPrefs.HasKey("PlayerUsername") && passToGame==1)
        {
            // player already register
            SceneManager.LoadScene("SampleScene");            
        }
        else
        {
            Debug.Log("Player not registered. Staying on main menu.");
        }
    }
}
