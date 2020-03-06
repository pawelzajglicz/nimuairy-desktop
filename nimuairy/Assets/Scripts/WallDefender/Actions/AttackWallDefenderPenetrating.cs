using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackWallDefenderParameters))]
public class AttackWallDefenderPenetrating : WallDefenderAction
{
    private AttackWallDefenderParameters attackParameters;

    private void Awake()
    {
        attackParameters = GetComponent<AttackWallDefenderParameters>();
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
