using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColliderDamage : MonoBehaviour
{
    public int damageAmount = 7;
    public bool attackActive = false; // Only allow damage during active attack

    private void OnTriggerEnter(Collider other)
    {
        if (attackActive && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Collision with active attack");
                player.TakeDamage(damageAmount);
            }
        }
    }
}
