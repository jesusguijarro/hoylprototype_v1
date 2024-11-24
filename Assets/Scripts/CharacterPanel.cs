using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health, level;
    [SerializeField] private Image healthFill, levelFill;
    [SerializeField] private Player player;
    
    // Stats
    private List<TextMeshProUGUI> playerStatTexts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI playerStatPrefab;
    [SerializeField] private Transform playerStatPanel;

    // Equipped Weapon
    [SerializeField] private Sprite defaultWeaponSprite;
    private PlayerWeaponController playerWeaponController;
    [SerializeField] private TextMeshProUGUI weaponStatPrefab;
    [SerializeField] private Transform weaponStatPanel;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private Image weaponIcon;
    private List<TextMeshProUGUI> weaponStatTexts = new List<TextMeshProUGUI>();

    void Awake()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;
    }

    void Start()
    {
        playerWeaponController = player.GetComponent<PlayerWeaponController>();
        InitializeStats();
    }   

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }
    void UpdateLevel()
    {
        if (player != null && player.PlayerLevel != null)
        {
            Debug.Log("player level: " + player.PlayerLevel.Level.ToString());
            this.level.text = player.PlayerLevel.Level.ToString();
            this.levelFill.fillAmount = (float)player.PlayerLevel.CurrentExperience / (float)player.PlayerLevel.RequiredExperience;
        }
        else
        {
            Debug.LogWarning("Player or PlayerLevel is null in CharacterPanel.");
        }
    }
    void InitializeStats()
    {       
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab));
            playerStatTexts[i].transform.SetParent(playerStatPanel);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {            
            playerStatTexts[i].text = player.characterStats.stats[i].StatName + ": " + player.characterStats.stats[i].GetCalculatedStatValue().ToString();
        }
    }

    void UpdateEquippedWeapon(Item item)
    {
        weaponIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
        weaponNameText.text = item.ItemName;

        //for (int i = 0; i < item.Stats.Count; i++)
        //{
        //    weaponStatTexts.Add(Instantiate(weaponStatPrefab));
        //    weaponStatTexts[i].transform.SetParent(weaponStatPanel);
        //    weaponStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        //}
        UpdateStats();
    }

    public void UnequipWeapon()
    {        
        weaponNameText.text = "Sin equipar";
        weaponIcon.sprite = defaultWeaponSprite;

        //for (int i = 0; i < weaponStatTexts.Count; i++)
        //{
        //    Destroy(weaponStatTexts[i].gameObject);
        //}
        //weaponStatTexts.Clear();
        playerWeaponController.UnequipWeapon();
    }
}
