using System;
using UnityEngine;

public class BoxColliderController : MonoBehaviour
{
    public static BoxColliderController Instance { get; private set; }
    [Header("Trigger Manual")]
    public BoxCollider manualTrigger;

    private void Awake()
    {
        // Garantizar que solo haya una instancia
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }
    public void TriggerActivate()
    {
            manualTrigger.isTrigger = true;
    }
 




}
