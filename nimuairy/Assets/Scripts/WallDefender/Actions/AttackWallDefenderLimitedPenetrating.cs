﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderLimitedPenetrating : WallDefenderAction
{
    // TODO: refactoring

    [SerializeField] int penetratesLimit = 1;
    [SerializeField] int currentPenetrates;

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
            ManagePenetratesLimit();
        }

        FieldDefenderMovement fieldDefenderMovement = collision.gameObject.GetComponent<FieldDefenderMovement>();
        if (fieldDefenderMovement != null && fieldDefenderMovement.GetComponent<StateManager>().IsInAttackingState())
        {
            DealDamageToFieldDefender(collision);
            ManagePenetratesLimit();
        }
    }

    private void ManagePenetratesLimit()
    {
        currentPenetrates++;
        if (currentPenetrates >= penetratesLimit)
        {
            Destroy(gameObject);
        }
    }

    private void DealDamageToEnemy(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackParameters.AttackPower * factorFromWallDefender);
        }
    }

    private void DealDamageToFieldDefender(Collider2D collision)
    {
        Health fieldDefenderHealth = collision.GetComponent<Health>();
        if (fieldDefenderHealth != null && collision)
        {
            fieldDefenderHealth.DealDamage(attackParameters.AttackPower * attackParameters.FieldDefenderDamagingFactor * factorFromWallDefender);
        }
    }
}
