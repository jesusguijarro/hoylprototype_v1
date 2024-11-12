using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColliderDamage : MonoBehaviour
{    
    public bool attackActive = false; // Only allow damage during active attack
    public int CurrentDamage { get; set; }
    public bool isAttacking { get; set; }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
        isAttacking = true;
    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enemy Collision");
        if (isAttacking && col.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();            
            player.TakeDamage(CurrentDamage);
            Debug.Log("Player hit with damage: " + CurrentDamage);
            isAttacking = false;
        }
    }
}
