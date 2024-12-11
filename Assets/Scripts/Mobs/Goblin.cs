using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : Interactable, IEnemy
{
    public ArmColliderDamage armColliderDamage;

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

    private Animator enemyAnimator;

    [SerializeField] private Healthbar _healthbar;

    private bool isDead = false; // Flag to prevent undesired behavior after death

    void Start()
    {
        Droptable = new DropTable();
        Droptable.loot = new List<LootDrop>
        {
            new LootDrop("key", 30),
        };

        ID = 0;
        Experience = 100;
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(6, 10, 2);
        currentHealth = maxHealth;
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (isDead) return; // If the goblin is dead, do nothing

        withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask); // Aggro radius
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        }
    }

    public void PerformAttack()
    {
        if (isDead) return; // Do not attack if dead

        Debug.Log("Perform Attack invoked");
        enemyAnimator.SetBool("isAttacking", true);
        if (armColliderDamage != null)
        {
            Debug.Log("PerformAttack");
            armColliderDamage.PerformAttack(10);
            StartCoroutine(ResetAttackActive(0.5f));
        }
        else Debug.Log("armColliderDamage is null");
    }

    IEnumerator ResetAttackActive(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyAnimator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Do not take damage if dead

        Debug.Log("Took damage.");
        currentHealth -= amount;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    void ChasePlayer(Player player)
    {
        this.player = player;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > navAgent.stoppingDistance)
        {
            navAgent.SetDestination(player.transform.position);
            enemyAnimator.SetBool("isMoving", true);  // Start running animation
        }
        else
        {
            enemyAnimator.SetBool("isMoving", false); // Stop running animation
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", 0.5f, 2f);
            }
        }
    }

    public void Die()
    {
        if (isDead) return; // Prevent multiple calls to Die

        isDead = true; // Mark as dead
        enemyAnimator.Play("Die");
        navAgent.isStopped = true;

        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        while (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return null; // Wait until the Die state is active
        }

        yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);

        CombatEvents.EnemyDied(this);
        Spawner.Respawn();
        Destroy(gameObject);
        DropLoot();
    }

    void DropLoot()
    {
        Debug.Log("DropLoot called.");
        Item item = Droptable.GetDrop();
        if (item != null)
        {
            PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
}
