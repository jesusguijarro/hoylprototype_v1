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
        if (other.CompareTag("Player")) // Asegúrate de que solo el jugador active la música
        {
            audioManager.EnterBattleZone();  // Activa la música de batalla
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.ExitBattleZone();  // Desactiva la música de batalla y reactiva la música de fondo
        }
    }
}
