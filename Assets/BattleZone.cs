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
        if (other.CompareTag("Player")) // Aseg�rate de que solo el jugador active la m�sica
        {
            audioManager.EnterBattleZone();  // Activa la m�sica de batalla
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.ExitBattleZone();  // Desactiva la m�sica de batalla y reactiva la m�sica de fondo
        }
    }
}
