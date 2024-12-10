using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHandWoman;
    public GameObject playerHandMan;
    private GameObject playerHand;
    public GameObject EquippedWeapon  { get; set; }

    //Transform spawnProjectile;
    Item currentEquippedItem;
    public Transform spawnProjectile;
    IWeapon equippedWeapon;
    CharacterStats characterStats;
    private void Start()
    {
        //spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().characterStats;

        string playerAppearance = PlayerPrefs.GetString("PlayerAppearance");
        if (playerAppearance == "MALE")
        {
            playerHand = playerHandMan.gameObject;
        }
        else if (playerAppearance == "FEMALE")
        {
            playerHand = playerHandWoman.gameObject;
        }
        else
        {
            Debug.LogError("PlayerAppearance value is invalid or missing in PlayerPrefs.");
        }

    }
    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null) 
        {
            UnequipWeapon();
        }       

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();        
        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;        
        EquippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.Stats = itemToEquip.Stats;
        currentEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);
        UIEventHandler.ItemEquippped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentEquippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
        Destroy(playerHand.transform.GetChild(0).gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        { 
            PerformWeaponAttack();
        }
    }

    public void PerformWeaponAttack()
    {
        equippedWeapon.PerformAttack(CalculateDamage());
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue()*2) 
            + Random.Range(2,8);
        damageToDeal += CalculateCrit(damageToDeal);
        //Debug.Log("Damage Dealt: " + damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if (Random.value <= .10f)
        {
            int critDagame = (int)(damage * Random.Range(.25f, .75f));
            return critDagame;
        }    
        return 0;
    }
}
