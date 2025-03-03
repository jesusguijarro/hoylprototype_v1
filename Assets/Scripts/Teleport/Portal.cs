using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : ActionItem
{
    public Vector3 TeleportLocation { get; set; } 
    [SerializeField]
    private Portal[] linkedPortals;
    private PortalController PortalController { get; set; }
    void Start()
    {
        PortalController = FindAnyObjectByType<PortalController>();
        TeleportLocation = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
    }

    public override void Interact()
    {
        //if (BoxColliderController.Instance.isTriger())
        //{
            PortalController.ActivatePortal(linkedPortals);
            playerAgent.ResetPath();
        //}


    }
}