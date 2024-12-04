using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TeleportOnTouch : MonoBehaviour
{
    [SerializeField]
    private string targetLocationName; // Nombre del objeto destino en la escena (configurable desde el prefab)

    private GameObject targetLocation; // Objeto destino encontrado en la escena
    private Player player;
    private NavMeshAgent playerAgent;

    private AudioManager audioManager;
    private GameObject panel;

    private void Awake()
    {
        // Buscar el AudioManager en la escena
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con la etiqueta 'Audio'.");
        }
    }

    void Start()
    {
        // Buscar el jugador din�micamente
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerAgent = player.GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto de tipo 'Player' en la escena.");
        }

        // Buscar el panel del portal dentro del objeto
        Transform panelTransform = transform.Find("Panel_Portal");
        if (panelTransform != null)
        {
            panel = panelTransform.gameObject;
        }
        

        // Buscar el objeto destino por nombre
        if (!string.IsNullOrEmpty(targetLocationName))
        {
            targetLocation = GameObject.Find(targetLocationName);
            if (targetLocation == null)
            {
                Debug.LogError($"No se encontr� ning�n objeto en la escena con el nombre '{targetLocationName}'.");
            }
        }
        else
        {
            Debug.LogWarning("El nombre del destino no ha sido configurado en el prefab.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetLocation != null)
            {
                // Desactivar NavMeshAgent antes de teletransportar
                playerAgent.enabled = false;

                // Reproducir el sonido de portal
                if (audioManager != null)
                {
                    audioManager.PlaySFX(audioManager.portal);
                }

                // Iniciar la transici�n y teletransportar al jugador
                StartCoroutine(LoadTransitionSceneAndTeleport(targetLocation.transform.position));
            }
            else
            {
                Debug.LogWarning("No se ha establecido un destino para el teletransporte.");
            }
        }
    }

    public IEnumerator LoadTransitionSceneAndTeleport(Vector3 targetPosition)
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

        // Teletransportar al jugador despu�s de la transici�n
        player.transform.position = targetPosition;
        Debug.Log($"Jugador teletransportado a: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");

        // Rehabilitar NavMeshAgent despu�s del teletransporte
        playerAgent.enabled = true;
        playerAgent.ResetPath();

        // Descargar la escena de transici�n
        SceneManager.UnloadSceneAsync("loadingScene");

        // Ocultar el panel si era necesario
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void ClosePortalPanel()
    {
        // Ocultar el panel
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
