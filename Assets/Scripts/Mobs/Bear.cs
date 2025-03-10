using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : Interactable, IEnemy
{
    public ArmColliderDamage headColliderDamage;

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
    private AudioManager audioManager;
    public int cont = 0;

    Animator enemyAnimator;

    //[SerializeField] private Healthbar _healthbar;

    private void Start()
    {
        audioManager = AudioManager.Instance;
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
        enemyAnimator.SetBool("isAttacking", true);
        //Debug.Log("damage to player");
        //player.TakeDamage(5);
        if (headColliderDamage != null)
        {
            Debug.Log("PerformAttack");
            // Activate damage only during this attack
            headColliderDamage.PerformAttack(10);
            //playerAnimator.SetTrigger("Attack"); // Trigger attack animation

            // Reset damage after animation duration (adjust timing to animation length)
            StartCoroutine(ResetAttackActive(0.5f));
        }
        else Debug.Log("legColliderDamage is null");
    }

    IEnumerator ResetAttackActive(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyAnimator.SetBool("isAttacking", false);
    }
    public void TakeDamage(int amount)
    {
        Debug.Log("Took damage.");
        currentHealth -= amount;
        //_healthbar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
            Die();
    }
    void ChasePlayer(Player player)
    {
        if (cont == 0)
        {
            StartCoroutine(AudioManager.Instance.SwitchToBattleMusic());
            cont++;
        }
        //enemyAnimator.Play("Roar");
        //wait after Roar ends
        this.player = player;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Update destination only if distance is larger than stopping distance
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
        StartCoroutine(AudioManager.Instance.SwitchToBackgroundMusic());
        //enemyAnimator.Play("Die");
        navAgent.isStopped = true;
        CombatEvents.EnemyDied(this);
        Spawner.Respawn();
        Destroy(gameObject);
        //StartCoroutine(DestroyAfterAnimation());
    }
    //private IEnumerator DestroyAfterAnimation()
    //{
    //    //DropLoot();
    //    // Wait until the animator is in the "Die" state to ensure the animation starts
    //    while (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
    //    {
    //        yield return null; // Wait until the Die state is active
    //    }

    //    // Wait for the full duration of the Die animation
    //    yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);

    //    // Now destroy the object after the animation finishes
    //    CombatEvents.EnemyDied(this);
    //    Spawner.Respawn();
    //    Destroy(gameObject);
    //}
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
