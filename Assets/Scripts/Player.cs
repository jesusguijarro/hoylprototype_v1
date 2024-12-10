using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; }

    [SerializeField] private GameObject playerManPrefab; // Prefab para el personaje masculino
    [SerializeField] private GameObject playerWomanPrefab; // Prefab para el personaje femenino
    [SerializeField] private GameObject revivePanel; // Referencia al panel de revivir
    [SerializeField] private float reviveOffset = 2f; // Distancia de desplazamiento al revivir

    private Vector3 deathPosition;
    private bool isRevived = false; // Indica si el jugador ha sido revivido

    void Start()
    {
        // Configura PlayerAppearance
        string playerAppearance = PlayerPrefs.GetString("PlayerAppearance");
        if (playerAppearance == "MALE")
        {
            playerManPrefab.SetActive(true);
            playerWomanPrefab.SetActive(false);
        }
        else if (playerAppearance == "FEMALE")
        {
            playerManPrefab.SetActive(false);
            playerWomanPrefab.SetActive(true);
        }
        else
        {
            Debug.LogError("PlayerAppearance value is invalid or missing in PlayerPrefs.");
        }

        PlayerLevel = GetComponent<PlayerLevel>();
        this.currentHealth = this.maxHealth;
        characterStats = new CharacterStats(10, 10, 10);
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);

        string playerUsername = PlayerPrefs.GetString("PlayerUsername");
        Debug.Log("Bienvenido, " + playerUsername + "!");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Die()
    {
        // Guarda la posición al morir
        deathPosition = transform.position;
        Debug.Log("Player dead. Showing revive panel.");
        revivePanel.SetActive(true);
    }

    public void Revive()
    {
        Debug.Log("Reviving player. Resetting health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);

        // Si ya ha sido revivido, no hacer nada más
        if (isRevived)
            return;

        // Aquí se llama al método de LoadPlayerPosition que recupera la penúltima posición guardada
        SavePlayerPosition savePlayerPosition = FindObjectOfType<SavePlayerPosition>(); // Obtener el script que guarda la posición
        if (savePlayerPosition != null)
        {
            savePlayerPosition.LoadPlayerPosition(); // Cargar la penúltima posición guardada
        }
        else
        {
            Debug.LogError("SavePlayerPosition component not found in the scene.");
        }

        isRevived = true; // Indica que el jugador ha sido revivido y se ha colocado en la nueva posición

        if (revivePanel != null)
        {
            revivePanel.SetActive(false); // Oculta el panel de revivir
        }
        else
        {
            Debug.LogError("Revive panel no encontrado.");
        }
    }
}
