using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackWallDefenderParameters))]
public class AttackWallDefenderContinuos : WallDefenderAction
{
    // TODO: refactoring

    [SerializeField] float attackTimeRate = 0.4f;
    private HashSet<GameObject> charactersToDealDamage;
    private AttackWallDefenderParameters attackParameters;

    private void Awake()
    {
        charactersToDealDamage = new HashSet<GameObject>();
        attackParameters = GetComponent<AttackWallDefenderParameters>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckEnemyCollision(collision);
        CheckFieldDefenderCollision(collision);
    }

    private void CheckEnemyCollision(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            DealDamageToEnemy(enemy);
        }
    }

    private void CheckFieldDefenderCollision(Collider2D collision)
    {
        FieldDefenderMovement fieldDefenderMovement = collision.gameObject.GetComponent<FieldDefenderMovement>();
        if (fieldDefenderMovement != null && fieldDefenderMovement.GetComponent<StateManager>().IsInAttackingState())
        {
            DealDamageToFieldDefender(fieldDefenderMovement);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckEnemyExitCollision(collision);
        CheckFieldDefenderExitCollision(collision);
    }

    private void CheckEnemyExitCollision(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            StopDealDamageToCharacter(enemy.gameObject);
        }
    }

    private void CheckFieldDefenderExitCollision(Collider2D collision)
    {
        FieldDefenderMovement fieldDefenderMovement = collision.gameObject.GetComponent<FieldDefenderMovement>();
        if (fieldDefenderMovement != null)
        {
            StopDealDamageToCharacter(fieldDefenderMovement.gameObject);
        }
    }

    private void StopDealDamageToCharacter(GameObject characterToDealDamage)
    {
        if (charactersToDealDamage.Contains(characterToDealDamage))
        {
            charactersToDealDamage.Remove(characterToDealDamage);
        }
    }

    private void DealDamageToEnemy(Enemy enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        if (enemyHealth != null)
        {         
            charactersToDealDamage.Add(enemy.gameObject);
            StartCoroutine(ProcessDealingDamageToEnemy(enemy));
        }
    }

    IEnumerator ProcessDealingDamageToEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(attackTimeRate);

        if (charactersToDealDamage.Contains(enemy.gameObject))
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.DealDamage(attackParameters.AttackPower);
            StartCoroutine(ProcessDealingDamageToEnemy(enemy));
        }
    }

    private void DealDamageToFieldDefender(FieldDefenderMovement fieldDefenderMovement)
    {
        Health enemyHealth = fieldDefenderMovement.GetComponent<Health>();
        if (enemyHealth != null)
        {
            charactersToDealDamage.Add(fieldDefenderMovement.gameObject);
            StartCoroutine(ProcessDealingDamageTFieldDefender(fieldDefenderMovement));
        }
    }

    IEnumerator ProcessDealingDamageTFieldDefender(FieldDefenderMovement fieldDefenderMovement)
    {
        yield return new WaitForSeconds(attackTimeRate);

        if (charactersToDealDamage.Contains(fieldDefenderMovement.gameObject))
        {
            Health fieldDefenderHealth = fieldDefenderMovement.GetComponent<Health>();
            fieldDefenderHealth.DealDamage(attackParameters.AttackPower * attackParameters.FieldDefenderDamagingFactor);
            StartCoroutine(ProcessDealingDamageTFieldDefender(fieldDefenderMovement));
        }
    }
}
