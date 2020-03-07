using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFieldDefender : MonoBehaviour
{
    [SerializeField] protected float attackPower = 10f;
    [SerializeField] protected float lifeTime = 0.2f;
    
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
            DealDamageToEnemy(collision);
        }
    }

    protected virtual void DealDamageToEnemy(Collider2D collision)
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
