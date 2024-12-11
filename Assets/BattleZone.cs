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
        // Asegúrate de que solo el jugador active la música
        if (other.CompareTag("Player"))  // Cambia "Player" por el Tag o Layer que uses para el jugador
        {
            audioManager.EnterBattleZone();  // Activa la música de batalla
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale de la zona de batalla
        if (other.CompareTag("Player"))  // Cambia "Player" por el Tag o Layer que uses para el jugador
        {
            audioManager.ExitBattleZone();  // Desactiva la música de batalla y reactiva la música de fondo
        }
    }
}
