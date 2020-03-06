using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackWallDefenderParameters))]
public class AttackWallDefenderContinuos : WallDefenderAction
{
    [SerializeField] float attackTimeRate = 0.4f;
    private HashSet<Enemy> enemyToDealDamage;
    private AttackWallDefenderParameters attackParameters;

    private void Awake()
    {
        enemyToDealDamage = new HashSet<Enemy>();
        attackParameters = GetComponent<AttackWallDefenderParameters>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            DealDamageToEnemy(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            StopDealDamageToEnemy(enemy);
        }
    }

    private void StopDealDamageToEnemy(Enemy enemy)
    {
        if (enemyToDealDamage.Contains(enemy))
        {
            enemyToDealDamage.Remove(enemy);
        }
    }

    private void DealDamageToEnemy(Enemy enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        if (enemyHealth != null)
        {
            
            enemyToDealDamage.Add(enemy);
            StartCoroutine(ProcessDealingDamageToEnemy(enemy));
        }
    }

    IEnumerator ProcessDealingDamageToEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(attackTimeRate);

        if (enemyToDealDamage.Contains(enemy))
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.DealDamage(attackParameters.GetAttackPower());
            StartCoroutine(ProcessDealingDamageToEnemy(enemy));
        }
    }
}
