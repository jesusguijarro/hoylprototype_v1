using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : Interactable, IEnemy
{
    public ArmColliderDamage armColliderDamage;  // Reference to arm collider script

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

    [SerializeField] private Healthbar _healthbar;

    void Start()
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

        if (_healthbar) Debug.Log("_healthbar exists");
        else Debug.Log("_healthbar doesn't exist");

        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

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
        if (armColliderDamage != null)
        {
            Debug.Log("Performing attack");
            // Activate damage only during this attack
            armColliderDamage.PerformAttack(10);

            // Reset attack after animation duration
            StartCoroutine(ResetAttackActive(0.5f));
        }
        else
        {
            Debug.Log("armColliderDamage is null");
        }
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
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);
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
        this.player = player;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > navAgent.stoppingDistance)
        {
            navAgent.SetDestination(player.transform.position);
            enemyAnimator.SetBool("isMoving", true);  // Start moving animation
        }
        else
        {
            enemyAnimator.SetBool("isMoving", false); // Stop moving animation
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", 0.5f, 2f);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(AudioManager.Instance.SwitchToBackgroundMusic());
        enemyAnimator.Play("Die");
        navAgent.isStopped = true;

        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        while (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);

        yield return StartCoroutine(ShowGuide());

        CombatEvents.EnemyDied(this);
        Spawner.Respawn();
        Destroy(gameObject);
    }

    private IEnumerator ShowGuide()
    {
        yield return new WaitForSeconds(3f);

        Sprite image = Resources.Load<Sprite>("UI/Icons/GuideUsage/fairy_happy");

        GuideUIManager.Instance.Parameters("Enemigo derrotado!", "Has derrotado al Golem de Hielo, dirigite con Bjorn en la casa cerca del puente del Norte!", image);
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
