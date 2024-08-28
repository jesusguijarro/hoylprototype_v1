using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{
    private Player player;
    Vector3 playerPosition;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();  // Ensure player is correctly assigned

        if (PlayerPrefs.HasKey("playerStarted"))
        {
            LoadPlayerPosition();
        }

        if (!PlayerPrefs.HasKey("playerStarted"))
        {
            PlayerPrefs.SetInt("playerStarted", 1);
            PlayerPrefs.Save();
        }
    }

    public void SavePosition()
    {
        playerPosition = player.transform.position;
        
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("playerPositionZ", playerPosition.z);
        PlayerPrefs.Save();

        Debug.Log("Player position saved!");
    }
    
    public void ResetPlayerPosition() // only use this for develop porpuse
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs reset!");
    }

    private void LoadPlayerPosition()
    {
        player.gameObject.SetActive(false);
        Vector3 savedPosition = new Vector3(
            PlayerPrefs.GetFloat("playerPositionX"),
            PlayerPrefs.GetFloat("playerPositionY"),
            PlayerPrefs.GetFloat("playerPositionZ")
        );
        player.transform.position = savedPosition;
        player.gameObject.SetActive(true);

        Debug.Log("Player position loaded!");
    }
}
