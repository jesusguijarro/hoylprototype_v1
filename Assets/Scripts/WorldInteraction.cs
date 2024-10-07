using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {   // GetMouseButtonDown(0) - left mouse button
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            GetInteraction();
    }

    void GetInteraction()
    {
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
