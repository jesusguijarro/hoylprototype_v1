using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : MonoBehaviour
{
    public ArmColliderDamage legColliderDamage;

    public LayerMask aggroLayerMask;
    public float currentHealth;
    public float maxHealth;
    public int ID { get; set; }
    public int Experience { get; set; }
    public DropTable Droptable { get; set; }
    public Spawner Spawner { get; set; }
    public PickupItem pickupItem;

    private Player player;
    private NavMeshAgent navAgent;
    private CharacterStats characterStats;
    private Collider[] withinAggroColliders;

    Animator enemyAnimator;

    //[SerializeField] private Healthbar _healthbar;

    private void Start()
    {
        Droptable = new DropTable();
        Droptable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 25),
            new LootDrop("staff", 25),
            new LootDrop("potion_log", 25)
        };
        ID = 0;
        Experience = 100;
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(6, 10, 2);
        currentHealth = maxHealth;

        //if (_healthbar) Debug.Log("_healthbar exists");
        //else Debug.Log("_healthbar don't exists");

        //_healthbar.UpdateHealthBar(maxHealth, currentHealth);       

        enemyAnimator = GetComponentInChildren<Animator>();
    }
}
