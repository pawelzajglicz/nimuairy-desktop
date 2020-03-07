using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackWallDefenderParameters))]
public class AttackWallDefenderPenetrating : WallDefenderAction
{
    // TODO: refactoring

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

        FieldDefenderMovement fieldDefenderMovement = collision.gameObject.GetComponent<FieldDefenderMovement>();
        if (fieldDefenderMovement != null && fieldDefenderMovement.GetComponent<StateManager>().IsInAttackingState())
        {
            DealDamageToFieldDefender(collision);
        }
    }

    private void DealDamageToEnemy(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackParameters.AttackPower);
        }
    }

    private void DealDamageToFieldDefender(Collider2D collision)
    {
        Health fieldDefenderHealth = collision.GetComponent<Health>();
        if (fieldDefenderHealth != null && collision)
        {
            fieldDefenderHealth.DealDamage(attackParameters.AttackPower * attackParameters.FieldDefenderDamagingFactor);
        }
    }
}
