using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    public float movementSpeed = 5f;
    public bool isUsingWASD = false; // Tracks if the player is using WASD keys
    Animator playerAnimator; // Reference to the animator
    Sword sword;

    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;

    private bool isJumping;

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        AssignAnimator(); // Get the animator component
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void AssignAnimator()
    {
        // Get the Animator from the active child prefab
        playerAnimator = GetComponentInChildren<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("Animator component not found on the active player prefab.");
        }
    }

    private void Update()
    {
        if (SimpleInput.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        if (SimpleInput.GetButtonDown("X") && isGrounded)
        {
            PerformAttack();
        }
        if (SimpleInput.GetButtonDown("C") && isGrounded)
        {
            Debug.Log("Botón C presionado");
            GetInteraction();
        }
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Movimiento con joystick o teclado
        if (SimpleInput.GetAxisRaw("Horizontal") != 0 || SimpleInput.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isUsingWASD = true;
            MoveWithJoystickOrWASD();
        }
        else
        {
            isUsingWASD = false;
        }

        // Interacción
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }

        // Ataque
        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformAttack();
        }

        // Actualizar animaciones
        UpdateAnimations();

        // Verificar estado de caída
        if (!isGrounded && rb.velocity.y < -0.1f && !IsGroundNearby())
        {
            playerAnimator.SetBool("isFalling", true);
        }
        else if (isGrounded || rb.velocity.y >= -0.1f)
        {
            playerAnimator.SetBool("isFalling", false);
        }
    }

    private bool IsGroundNearby()
    {
        // Detectar proximidad al suelo
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void MoveWithJoystickOrWASD()
    {
        if (playerAgent.enabled)
        {
            // Desactivar el pathfinding del NavMeshAgent mientras se usa el joystick o WASD
            playerAgent.updateRotation = false;
            playerAgent.isStopped = true;
        }

        // Obtener los valores de movimiento del joystick o teclado
        float moveHorizontal = SimpleInput.GetAxis("Horizontal");
        float moveVertical = SimpleInput.GetAxis("Vertical");

        // Calcular la dirección de movimiento
        Vector3 movementDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;

        // Aplicar el movimiento a la posición del jugador
        transform.Translate(movement, Space.World);

        // Rotar al jugador en la dirección del movimiento
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void Jump()
    {
        playerAnimator.SetBool("isGrounded", false);
        isGrounded = false;
        playerAgent.enabled = false; // Desactivar NavMeshAgent durante el salto
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Resetear velocidad vertical antes del salto
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Colisión de alta precisión
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool("isJumping", true);
        isJumping = true;
    }

    void PerformAttack()
    {
        playerAnimator.SetBool("isAttacking", true);
        if (sword != null) // Verificar que la espada existe
        {
            sword.PerformAttack(10); // Iniciar el ataque
            Debug.Log("Attack triggered");
        }
        // Resetear ataque después de un breve retraso
        StartCoroutine(ResetAttackAfterDelay(0.5f));
    }

    IEnumerator ResetAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerAnimator.SetBool("isAttacking", false);
    }

    void GetInteraction()
    {
        playerAgent.isStopped = false; // Habilitar movimiento del NavMeshAgent
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            playerAgent.updateRotation = true;
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Enemy")
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else if (interactedObject.tag == "Interactable Object")
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                playerAgent.stoppingDistance += 2.5f;
            }
            //else
            //{
            //    playerAgent.stoppingDistance = 0;
            //    playerAgent.destination = interactionInfo.point;
            //}
        }
    }

    void UpdateAnimations()
    {
        if (isUsingWASD)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
        }

        if (playerAgent.velocity.magnitude > 0.1f && !isUsingWASD)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else if (!isUsingWASD)
        {
            playerAnimator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || IsGroundNearby())
        {
            playerAnimator.SetBool("isGrounded", true);
            isGrounded = true;
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isFalling", false);
            isJumping = false;

            if (!playerAgent.enabled)
            {
                playerAgent.enabled = true;
            }

            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        else if (isJumping && collision.gameObject.CompareTag("Enemy"))
        {
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse); // Apply a downward force
        }
    }
}
