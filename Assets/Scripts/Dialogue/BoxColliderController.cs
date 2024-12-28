using UnityEngine;

public class BoxColliderController : MonoBehaviour
{
    [Header("Enemigo")]
    public GameObject enemy; // Referencia al enemigo
    public BoxCollider fightTrigger; // Trigger para despu�s de la pelea

    [Header("NPC")]
    public GameObject npc; // Referencia al NPC
    public BoxCollider npcTrigger; // Trigger para despu�s de hablar con el NPC
    private bool hasTalkedToNpc = false; // Indica si ya se habl� con el NPC

    void Start()
    {
        // Desactivar los triggers al inicio
        if (fightTrigger != null)
            fightTrigger.enabled = false;

        if (npcTrigger != null)
            npcTrigger.enabled = false;
    }

    void Update()
    {
        // Activar el trigger de pelea si el enemigo ha sido derrotado
        if (enemy == null && fightTrigger != null && !fightTrigger.enabled)
        {
            fightTrigger.enabled = true;
            Debug.Log("Trigger de pelea activado: El enemigo ha sido derrotado.");
        }

        // Activar el trigger del NPC tras la conversaci�n
        if (hasTalkedToNpc && npcTrigger != null && !npcTrigger.enabled)
        {
            npcTrigger.enabled = true;
            Debug.Log("Trigger del NPC activado: Se complet� la conversaci�n.");
        }
    }

    public void OnNpcConversationStart()
    {
        // Desactivar el trigger del NPC al iniciar la conversaci�n
        if (npcTrigger != null)
        {
            npcTrigger.enabled = false;
        }
        Debug.Log("Conversaci�n con NPC iniciada: Trigger desactivado.");
    }

    public void OnNpcConversationEnd()
    {
        // Activar el trigger del NPC tras la conversaci�n
        hasTalkedToNpc = true;
        if (npcTrigger != null)
        {
            npcTrigger.enabled = true; // Activar solo despu�s de toda la interacci�n
        }
        Debug.Log("Conversaci�n con NPC terminada: Trigger activado.");
    }
}
