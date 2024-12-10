using UnityEngine;

public class RevivePanelController : MonoBehaviour
{
    // Referencia al objeto del jugador
    private Player player;

    private void Start()
    {
        // Busca el componente Player en la escena
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("No se encontró el componente Player en la escena.");
        }

        // Oculta el panel al inicio
        gameObject.SetActive(false);
    }

    // Método para revivir al jugador
    public void OnReviveButtonClicked()
    {
        if (player != null)
        {
            player.Revive(); // Llama al método Revive del script Player
        }
    }
}
