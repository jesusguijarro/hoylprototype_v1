using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TeleportOnTouch : MonoBehaviour
{
    [SerializeField]
    private Transform targetLocation; // Lugar de destino en el Inspector, directamente asignado
    private Player player;
    private NavMeshAgent playerAgent;

    private AudioManager audioManager;
    private GameObject panel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        playerAgent = player.GetComponent<NavMeshAgent>();
        panel = transform.Find("Panel_Portal").gameObject;
    }

    // Este método se llama cuando el jugador entra en el área de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactivar NavMeshAgent antes de teletransportar
            playerAgent.enabled = false;

            // Reproducir el sonido de portal
            audioManager.PlaySFX(audioManager.portal);

            // Iniciar la transición y teletransportar al jugador
            StartCoroutine(LoadTransitionSceneAndTeleport(targetLocation.position));
        }
    }

    // Método para cargar la escena de transición y teletransportar al jugador
    public IEnumerator LoadTransitionSceneAndTeleport(Vector3 targetPosition)
    {
        // Cargar la escena de transición de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("loadingScene", LoadSceneMode.Additive);

        // Esperar a que la escena de transición se cargue completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Mostrar la escena de transición durante 3 segundos
        yield return new WaitForSeconds(3f);

        // Teletransportar al jugador después de la transición
        player.transform.position = targetPosition;
        Debug.Log($"Jugador teletransportado a: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");

        // Rehabilitar NavMeshAgent después del teletransporte
        playerAgent.enabled = true;
        playerAgent.ResetPath();

        // Descargar la escena de transición
        SceneManager.UnloadSceneAsync("loadingScene");

        // Limpiar los botones y ocultar el panel si era necesario
        panel.SetActive(false);
    }

    // Este método podría cerrarse si deseas ocultar el panel de portales manualmente
    public void ClosePortalPanel()
    {
        // Ocultar el panel
        panel.SetActive(false);
    }
}
