using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameUIManager : MonoBehaviour
{
    public static EndgameUIManager Instance { get; private set; }

    public GameObject endgamePanel;
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
        endgamePanel.SetActive(false);
    }

    public void Enable()
    {
        endgamePanel.SetActive(true);
    }

}
