using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Button button;
    private Portal[] portal;
    private Player player;
    private NavMeshAgent playerAgent; // new file
    private GameObject panel;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        playerAgent = player.GetComponent<NavMeshAgent>(); // new line
        panel = transform.Find("Panel_Portal").gameObject;
    }

    public void ActivatePortal(Portal[] portals)
    {
        if (panel.activeSelf)
        {
            return;
        }
        panel.SetActive(true);
        for (int i = 0; i < portals.Length; i++)
        {
            Button portalButton = Instantiate(button, panel.transform);
            portalButton.GetComponentInChildren<TextMeshProUGUI>().text = portals[i].name;
            int x = i;
            portalButton.onClick.AddListener(delegate { OnPortalButtonClick(x, portals[x]); });
        }
    }

    void OnPortalButtonClick(int portalIndex, Portal portal)
    {
        audioManager.PlaySFX(audioManager.portal);
        player.transform.position = portal.TeleportLocation;
        playerAgent.ResetPath();        
        foreach (Button button in GetComponentsInChildren<Button>()) 
        {
            Destroy(button.gameObject);
        }
        panel.SetActive(false);
    }

    public void ClosePortalPanel() // new method
    {
        // Destroy any remaining buttons
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
        // Hide the panel
        panel.SetActive(false);
    }
}
