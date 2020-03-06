using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFieldDefender : MonoBehaviour
{
    [SerializeField] float attackPower = 10f;
    [SerializeField] float lifeTime = 0.2f;
    
    private void Awake()
    {
        attackPower *= FindObjectOfType<StateManager>().GetAttackPowerModifier();    
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public float GetLifeTime()
    {
        return lifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Collided with power: " + attackPower);
            DealDamageToEnemy(collision);
        }
    }

    private void DealDamageToEnemy(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackPower);
        }
    }

    internal void ModifyAttackPowerByFactor(float attackPowerFactor)
    {
        attackPower *= attackPowerFactor;
    }
}
