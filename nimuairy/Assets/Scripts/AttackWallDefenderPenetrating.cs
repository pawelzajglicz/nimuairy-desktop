using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackParameters))]
public class AttackWallDefenderPenetrating : AttackWallDefender
{
    private AttackParameters attackParameters;

    private void Awake()
    {
        attackParameters = GetComponent<AttackParameters>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            DealDamageToEnemy(collision);
        }
    }

    private void DealDamageToEnemy(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackParameters.GetAttackPower());
        }
    }
}
