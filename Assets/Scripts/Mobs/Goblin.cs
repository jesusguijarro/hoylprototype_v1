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

    [SerializeField] private AudioClip chaseAudio; // Referencia al AudioClip
    private AudioSource audioSource; // AudioSource para reproducir el audio
    private AudioManager globalAudioManager;
    Animator enemyAnimator;

    [SerializeField] private Healthbar _healthbar;

    private bool isDead = false; // Bandera para evitar comportamientos no deseados tras la muerte

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

        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        globalAudioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        if (isDead) return; // Si el goblin está muerto, no hace nada

        withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask); // aggro radius
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        }
    }

    public void PerformAttack()
    {
        if (isDead) return; // No atacar si está muerto

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
        if (isDead) return; // No recibir daño si está muerto

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

        // Reproducir la música de persecución con un fade-in
        if (chaseAudio != null && !audioSource.isPlaying)
        {
            audioSource.clip = chaseAudio;
            StartCoroutine(FadeInAudio(audioSource, 1.0f, 0.5f)); // Fade-in a volumen 0.5
        }

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
        if (isDead) return; // Evitar múltiples llamadas a Die

        isDead = true; // Marcar como muerto
        enemyAnimator.Play("Die");
        navAgent.isStopped = true;

        // Detener audio inmediatamente y realizar fade-out
        StartCoroutine(FadeOutAudio(audioSource, 0.5f, () =>
        {
            audioSource.Stop();
            globalAudioManager.ResumeMusic(); // Reanudar música principal después del fade-out
        }));

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

    private IEnumerator FadeInAudio(AudioSource source, float duration, float targetVolume)
    {
        float startVolume = 0f;
        source.volume = startVolume;
        source.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume; // Asegúrate de alcanzar el volumen objetivo
    }

    private IEnumerator FadeOutAudio(AudioSource source, float duration, System.Action onComplete = null)
    {
        float startVolume = source.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        source.volume = 0; // Asegúrate de que el volumen sea 0 al finalizar
        onComplete?.Invoke(); // Llama al callback si se proporcionó
    }
}
