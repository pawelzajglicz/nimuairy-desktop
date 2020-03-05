using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] EnemyAttack attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FieldDefenderMovement fieldDefender = collision.GetComponent<FieldDefenderMovement>();

        if (fieldDefender)
        {
            AttackFieldDefender(fieldDefender);
        }
    }

    private void AttackFieldDefender(FieldDefenderMovement fieldDefender)
    {
        InstantiateAttack();
        DealDamage(fieldDefender);
    }

    private void InstantiateAttack()
    {
        EnemyAttack attackInstance = Instantiate(attack, transform.position, Quaternion.identity) as EnemyAttack;
        attackInstance.transform.parent = transform;
    }

    private void DealDamage(FieldDefenderMovement fieldDefender)
    {
        Health fieldDefenderHealth = fieldDefender.GetComponent<Health>();
        if (fieldDefenderHealth)
        {
            fieldDefenderHealth.DealDamage(attack.GetAttackPower());
        }
    }
}
