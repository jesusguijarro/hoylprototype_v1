using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : Interactable, IEnemy
{
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
    private Collider attackCollider;
    private ArmColliderDamage armColliderDamage;  // Reference to arm collider script
    void Start()
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

        attackCollider = transform.Find("AttackCollider").GetComponent<Collider>();
        attackCollider.isTrigger = true;  // Ensure this collider only handles damage as a trigger        
    }

    void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask); // aggro radius
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        }
    }
    public void PerformAttack()
    {
        //Debug.Log("damage to player");
        //player.TakeDamage(5);
        if (attackCollider != null)
        {
            Debug.Log("PerformAttack");
            // Activate damage only during this attack
            armColliderDamage.attackActive = true;
            //playerAnimator.SetTrigger("Attack"); // Trigger attack animation

            // Reset damage after animation duration (adjust timing to animation length)
            StartCoroutine(ResetAttackActive(0.5f)); // Adjust timing as needed
        }
        else Debug.Log("armColliderDamage is null");
    }

    IEnumerator ResetAttackActive(float delay)
    {
        yield return new WaitForSeconds(delay);
        armColliderDamage.attackActive = false;
    }

public void TakeDamage(int amount)
    {
        Debug.Log("Took damage.");
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }
    void ChasePlayer(Player player)
    {
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
                InvokeRepeating("PerformAttack", .5f, 2f);
        }
        else
        {
            CancelInvoke("PerformAttack");
        }
    }

    public void Die()
    {
        //DropLoot();
        CombatEvents.EnemyDied(this);
        this.Spawner.Respawn();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        Item item = Droptable.GetDrop();
        if (item != null)
        {
            PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
}
