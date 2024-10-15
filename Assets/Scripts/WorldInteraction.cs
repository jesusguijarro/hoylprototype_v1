using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    public float movementSpeed = 5f;
    public bool isUsingWASD = false; // Tracks if the player is using WASD keys
        

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {   // GetMouseButtonDown(0) - left mouse button
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) 
        { 
                isUsingWASD=true;
                MoveWithWASD();
        }
        else 
        {
                isUsingWASD = false;       
        }

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            GetInteraction();
    }

    void MoveWithWASD() {
        
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
}
