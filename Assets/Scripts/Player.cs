using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; }

    void Start()
    {        
        PlayerLevel = GetComponent<PlayerLevel>();
        this.currentHealth = this.maxHealth;
        characterStats = new CharacterStats(10,10,10);
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
        Debug.Log("Player dead. Reset health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
