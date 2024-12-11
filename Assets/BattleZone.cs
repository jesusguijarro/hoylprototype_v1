using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>(); // Encuentra el AudioManager
    }

    void OnTriggerEnter(Collider other)
    {
        // Aseg�rate de que solo el jugador active la m�sica
        if (other.CompareTag("Player"))  // Cambia "Player" por el Tag o Layer que uses para el jugador
        {
            audioManager.EnterBattleZone();  // Activa la m�sica de batalla
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale de la zona de batalla
        if (other.CompareTag("Player"))  // Cambia "Player" por el Tag o Layer que uses para el jugador
        {
            audioManager.ExitBattleZone();  // Desactiva la m�sica de batalla y reactiva la m�sica de fondo
        }
    }
}
