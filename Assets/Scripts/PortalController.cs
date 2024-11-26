using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

        // Desactivar NavMeshAgent antes de teletransportar
        playerAgent.enabled = false;

        // Iniciar la transici�n y pasar la posici�n del portal como par�metro
        StartCoroutine(LoadTransitionSceneAndTeleport(portal.TeleportLocation));
    }

    private IEnumerator LoadTransitionSceneAndTeleport(Vector3 targetPosition)
    {
        // Cargar la escena de transici�n de manera as�ncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("loadingScene", LoadSceneMode.Additive);

        // Esperar a que la escena de transici�n se cargue completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Mostrar la escena de transici�n durante 3 segundos
        yield return new WaitForSeconds(3f);

        // Teletransportar al jugador DESPU�S de la transici�n
        player.transform.position = targetPosition;
        Debug.Log($"Teleportado a: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");

        // Rehabilitar NavMeshAgent despu�s del teletransporte
        playerAgent.enabled = true;
        playerAgent.ResetPath();

        // Descargar la escena de transici�n
        SceneManager.UnloadSceneAsync("loadingScene");

        // Limpiar los botones y ocultar el panel
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