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

    private SavePlayerPosition savePlayerPosition;

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

        // Obtener el script SavePlayerPosition
        savePlayerPosition = FindAnyObjectByType<SavePlayerPosition>();
        if (savePlayerPosition == null)
        {
            Debug.LogError("SavePlayerPosition script not found in the scene.");
        }
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
        Debug.Log("Player dead. Showing revive panel.");
        revivePanel.SetActive(true);
    }

    public void Revive()
    {
        Debug.Log("Reviving player. Resetting health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);

        // Cargar la penúltima posición guardada
        if (savePlayerPosition != null)
        {
            savePlayerPosition.LoadPlayerPosition();
        }
        else
        {
            Debug.LogError("SavePlayerPosition component not found. Unable to load position.");
        }

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
