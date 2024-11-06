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
        // GetMouseButtonDown(0) - left mouse button
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { Jump();}

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isUsingWASD = true;
            MoveWithWASD();
        }
        else
        {
            isUsingWASD = false;
        }

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) GetInteraction();

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.X)) { PerformAttack(); }

        UpdateAnimations();

        // Check for falling state
        if (!isGrounded && rb.velocity.y < 0) { playerAnimator.SetBool("isFalling", true); }

    }

    void Jump()
    {
        playerAnimator.SetBool("isGrounded", false);
        isGrounded = false;
        playerAgent.enabled = false; // Disable NavMeshAgent during jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool("isJumping", true);
        isJumping = true;
        Debug.Log("Jump Action");       
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnimator.SetBool("isGrounded", true);
            isGrounded = true;
            playerAnimator.SetBool("isJumping", false);            
            playerAnimator.SetBool("isFalling", false);
            isJumping = false;
            playerAgent.enabled = true; // Re-enable NavMeshAgent when grounded
            Debug.Log("isGrounded" + isGrounded);
        }        
    }
}