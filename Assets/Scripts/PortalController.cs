using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Asegúrate de agregar esta línea

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Button button;
    private Portal[] portal;
    private Player player;
    private NavMeshAgent playerAgent; // new file
    private GameObject panel;

    AudioManager audioManager;

    // Variable estática para almacenar la última posición de teletransporte
    public static Vector3 teleportPosition;

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

        // Desactivar el NavMeshAgent antes del teletransporte
        playerAgent.enabled = false;

        // Guardar la escena original
        string originalScene = SceneManager.GetActiveScene().name;

        // Guardar la posición actual del jugador
        teleportPosition = player.transform.position;

        // Teletransportar al jugador antes de la transición
        player.transform.position = portal.TeleportLocation;

        // Cargar la escena de transición de manera asíncrona
        StartCoroutine(LoadTransitionSceneAndTeleport(originalScene, portal));
    }

    private IEnumerator LoadTransitionSceneAndTeleport(string originalScene, Portal portal)
    {
        // Cargar la escena de transición de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("loadingScene", LoadSceneMode.Additive);

        // Asegurarse de que la escena de carga se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Hacer que espere 2 segundos antes de regresar
        yield return new WaitForSeconds(3f);

        // Regresar a la escena original
        SceneManager.UnloadSceneAsync("loadingScene");
        SceneManager.LoadScene(originalScene);

        // Asegurarse de que el jugador y el NavMeshAgent estén activos
        player.gameObject.SetActive(true);
        playerAgent.enabled = true;

        // Restaurar la posición del jugador desde la variable estática
        player.transform.position = teleportPosition;

        // Rehabilitar el NavMeshAgent después de volver
        playerAgent.ResetPath();

        // Destruir los botones y ocultar el panel
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
