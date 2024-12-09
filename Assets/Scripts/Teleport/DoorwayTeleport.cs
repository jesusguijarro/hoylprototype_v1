using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DoorwayTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform targetLocation; // Lugar de destino en el Inspector, directamente asignado
    private Player player;
    private NavMeshAgent playerAgent;

    private AudioManager audioManager;
    private GameObject panel;

    private void Awake()
    {
        // Buscar el AudioManager y manejar posibles errores si no se encuentra
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager no encontrado en la escena.");
        }
    }

    void Start()
    {
        // Buscar al jugador y al NavMeshAgent
        player = FindAnyObjectByType<Player>();
        playerAgent = player.GetComponent<NavMeshAgent>();

        // Buscar el panel de portal
        panel = transform.Find("Panel_Portal")?.gameObject;
        if (panel == null)
        {
            Debug.LogError("No se encontró el Panel_Portal en el objeto.");
        }

        // Comprobar que targetLocation no sea nulo
        if (targetLocation == null)
        {
            Debug.LogError("La ubicación de destino (targetLocation) no está asignada.");
        }
    }

    // Este método se llama cuando el jugador entra en el área de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetLocation == null)
            {
                Debug.LogError("La ubicación de destino (targetLocation) es nula. No se puede teleportar.");
                return; // Detiene la ejecución si no hay destino asignado
            }

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

        // Forzar la posición del jugador al área objetivo
        player.transform.position = targetPosition;
        Debug.Log($"Jugador teletransportado a: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");

        // Si el NavMesh no es accesible, no habilitar el NavMeshAgent
        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            playerAgent.enabled = true;
            playerAgent.Warp(hit.position); // Ajustar el agente al NavMesh
        }
        else
        {
            Debug.LogWarning("El destino no es navegable. El NavMeshAgent permanecerá desactivado.");
            playerAgent.enabled = false;
        }

        // Descargar la escena de transición
        SceneManager.UnloadSceneAsync("loadingScene");

        // Ocultar el panel del portal si es necesario
        panel.SetActive(false);
    }

    // Este método podría cerrarse si deseas ocultar el panel de portales manualmente
    public void ClosePortalPanel()
    {
        // Ocultar el panel
        panel.SetActive(false);
    }
}
