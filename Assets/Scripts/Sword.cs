using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    //private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public int CurrentDamage { get; set; }
    public bool isAttacking { get; set; } // New property to track attack state

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
        isAttacking = true;
        //animator.SetTrigger("Base_Attack");
    }

    void OnTriggerEnter(Collider col)
    {
        if (isAttacking && col.CompareTag("Enemy"))
        {
            col.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
            Debug.Log("Enemy hit with damage: " + CurrentDamage);
            isAttacking = false; // Reset attack state to avoid multiple hits
        }
    }
}
