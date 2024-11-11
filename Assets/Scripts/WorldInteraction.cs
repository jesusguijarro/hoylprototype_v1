using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    public float movementSpeed = 5f;
    public bool isUsingWASD = false; // tracks if the player is using WASD keys
    Animator playerAnimator; // reference to the animator
    Sword sword;

    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;

    private bool isJumping;

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponentInChildren<Animator>(); // get the animator component
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isUsingWASD = true;
            MoveWithWASD();
        }
        else
        {
            isUsingWASD = false;
        }

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformAttack();
        }

        UpdateAnimations();

        // Check for falling state using both y-velocity and ground proximity
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
        // Use a Raycast to detect if the player is close to the ground
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void Jump()
    {
        playerAnimator.SetBool("isGrounded", false);
        isGrounded = false;
        playerAgent.enabled = false; // Disable NavMeshAgent during jump
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity before jump
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // High-precision collision
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool("isJumping", true);
        isJumping = true;
    }

    void PerformAttack()
    {
        playerAnimator.SetBool("isAttacking", true);
        if (sword != null) // Separate check to ensure sword exists
        {
            //playerAnimator.SetBool("isAttacking", true); // Immediately set isAttacking to true
            sword.PerformAttack(10); // Initiate the attack logic
            Debug.Log("Attack triggered");
        }
        // Reset attack after a delay (adjust to the animation length if needed)
        StartCoroutine(ResetAttackAfterDelay(0.5f));
    }

    IEnumerator ResetAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerAnimator.SetBool("isAttacking", false);
    }

    void MoveWithWASD()
    {

        // Disable NavMeshAgent's pathfinding while using WASD movement
        playerAgent.updateRotation = false;
        playerAgent.isStopped = true;

        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;

        // Aply movement to the player's position
        transform.Translate(movement, Space.World);

        // Rotate the player in the direction of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

    }

    void GetInteraction()
    {
        // Enable NavMeshAgent's pathfinding when clicking for movement
        playerAgent.isStopped = false;
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
                Debug.Log("Stopping distance: " + playerAgent.stoppingDistance);
                Debug.Log("Distance to interactable object: " + Vector3.Distance(playerAgent.transform.position, interactedObject.transform.position));
            }
            else
            {
                playerAgent.stoppingDistance = 0;
                playerAgent.destination = interactionInfo.point;
            }
        }
    }

    void UpdateAnimations()
    {
        // If player is using WASD, play the walking animation
        if (isUsingWASD)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else
            playerAnimator.SetBool("isMoving", false);

        // If the NavMeshAgent is moving, trigger walking animation
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
            playerAgent.enabled = true; // Re-enable NavMeshAgent when grounded

            // Revert to default collision detection when grounded
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        else if (isJumping && collision.gameObject.CompareTag("Enemy"))
        {
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse); // Apply a downward force
        }
    }
}