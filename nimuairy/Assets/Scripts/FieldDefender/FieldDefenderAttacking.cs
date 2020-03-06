using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldDefenderMovement))]
public class FieldDefenderAttacking : MonoBehaviour
{
    [SerializeField] AttackFieldDefender quickAttack;
    [SerializeField] AttackFieldDefender slowAttack;

    [SerializeField] float horizontalXAttackInterspace = 0.3f;
    [SerializeField] float attackPowerFactor = 1f;

    [SerializeField] bool isNowAttacking = false;

    FieldDefenderMovement fieldDefenderMovement;

    private void Awake()
    {
        fieldDefenderMovement = GetComponent<FieldDefenderMovement>();
    }

    void Update()
    {
        HandleAttacking();
    }

    private void HandleAttacking()
    {
        if (!isNowAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            MakeQuickAttack();
        }

        if (Input.GetKey(KeyCode.X))
        {
            MakeSlowAttack();
        }
    }

    private void MakeQuickAttack()
    {
        MakeAttack(quickAttack);
    }

    private void MakeSlowAttack()
    {
        MakeAttack(slowAttack);
    }

    private void MakeAttack(AttackFieldDefender attack)
    {
        AttackFieldDefender attackInstance = InstantiateAttack(attack);
        ManageAttackingState(attackInstance);
    }

    private void ManageAttackingState(AttackFieldDefender attackInstance)
    {
        float attackingDisabledTime = attackInstance.GetLifeTime();
        isNowAttacking = true;
        StartCoroutine(ManageAttackingDisabledTime(attackingDisabledTime));
    }

    private AttackFieldDefender InstantiateAttack(AttackFieldDefender attack)
    {
        float attackXPosition = transform.position.x + (horizontalXAttackInterspace * fieldDefenderMovement.GetFacingRightValue());
        AttackFieldDefender attackInstance = Instantiate(attack, new Vector2(attackXPosition, transform.position.y), Quaternion.identity);

        attackInstance.ModifyAttackPowerByFactor(attackPowerFactor);
        attackInstance.transform.parent = transform;
        return attackInstance;
    }

    IEnumerator ManageAttackingDisabledTime(float attackingDisabledTime)
    {
        yield return new WaitForSeconds(attackingDisabledTime);
        isNowAttacking = false;
    }

    internal void ModifyAttackPowerFactorByFactor(float attackPowerFactor)
    {
        this.attackPowerFactor *= attackPowerFactor;
    }
}
