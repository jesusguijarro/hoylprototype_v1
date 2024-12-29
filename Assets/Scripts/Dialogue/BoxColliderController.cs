using System;
using UnityEngine;

public class BoxColliderController : MonoBehaviour
{
    public static BoxColliderController Instance { get; private set; }

    [Header("Enemigo")]
    public GameObject enemy; // Referencia al enemigo
    public BoxCollider fightTrigger; // Trigger para después de la pelea

    [Header("Trigger Manual")]
    public GameObject manualTrigger; // Trigger que se activa/desactiva manualmente con la tecla E
    private bool Active;

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
        // Desactivar los triggers al inicio
        if (fightTrigger != null)
            fightTrigger.enabled = false;
        Active = false;
    }

    void Update()
    {
        // Activar el trigger de pelea si el enemigo ha sido derrotado
        if (enemy == null && fightTrigger != null && !fightTrigger.enabled)
        {
            fightTrigger.enabled = true;
            Debug.Log("Trigger de pelea activado: El enemigo ha sido derrotado.");
        }
    }
    public bool isTriger()
    {
        if (Active) return true;
        else return false;
        
    }
    public bool TriggerActivate()
    {
        Active =  true ;
        return Active;

    }

    
    
}
