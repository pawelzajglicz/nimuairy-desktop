using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldDefenderMovement))]
public class FieldDefenderAttacking : Paramizable
{
    [SerializeField] internal AttackFieldDefender quickAttack;
    [SerializeField] internal AttackFieldDefender slowAttack;

    [SerializeField] float horizontalXAttackInterspace = 0.3f;
    [SerializeField] float baseAttackPowerFactor = 1f;
    [SerializeField] float attackPowerFactor = 1f;
    [SerializeField] Param attackPowerFactorParam;

    [SerializeField] State state;
    [SerializeField] bool isNowAttacking = false;

    FieldDefenderMovement fieldDefenderMovement;
    FieldAttackManager fieldAttackManager;
    BuffWallDefender buff;
    GameManager gameManager;
    StateManager stateManager;

    private void Awake()
    {
        fieldDefenderMovement = GetComponent<FieldDefenderMovement>();
        fieldAttackManager = GetComponent<FieldAttackManager>();
        buff = EmptyBuff();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        stateManager = GetComponent<StateManager>();
        if (stateManager)
        {
            UpdateByState(stateManager.getCurrentState());
        }
    }

    void Update()
    {
        HandleAttacking();
    }

    public override void UpdateParams()
    {
        baseAttackPowerFactor = attackPowerFactorParam.paramValue * 0.1f + 1;
        attackPowerFactor = baseAttackPowerFactor;
    }

    private void HandleAttacking()
    {
        if (!isNowAttacking && gameManager.IsBattle)
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

    internal void SetBuff(BuffWallDefender buffWallDefender)
    {
        buff = buffWallDefender;

        attackPowerFactor = baseAttackPowerFactor * buff.attackBuffFactor * state.attackPowerModifier;
    }

    internal void TakeBuff(BuffWallDefender buffWallDefender)
    {
        buff = EmptyBuff();
        attackPowerFactor = baseAttackPowerFactor * state.attackPowerModifier;
    }

    private BuffWallDefender EmptyBuff()
    {
        BuffWallDefender buffWallDefender = new BuffWallDefender();
        buffWallDefender.speedBuffFactor = 1f;
        buffWallDefender.attackBuffFactor = 1f;

        return buffWallDefender;
    }

    private void MakeQuickAttack()
    {
        MakeAttack(quickAttack);
        fieldAttackManager.PushQuickAttack(quickAttack);
    }

    private void MakeSlowAttack()
    {
        MakeAttack(slowAttack);
        fieldAttackManager.PushSlowAttack(slowAttack);
    }

    private AttackFieldDefender MakeAttack(AttackFieldDefender attack)
    {
        AttackFieldDefender attackInstance = InstantiateAttack(attack);
        ManageAttackingState(attackInstance);
        return attackInstance;
    }

    private void ManageAttackingState(AttackFieldDefender attackInstance)
    {
        float attackingDisabledTime = attackInstance.GetLifeTime();
        isNowAttacking = true;
        StartCoroutine(ManageAttackingDisabledTime(attackingDisabledTime));
    }

    private AttackFieldDefender InstantiateAttack(AttackFieldDefender attack)
    {
        float attackXPosition = transform.position.x + (horizontalXAttackInterspace * fieldDefenderMovement.FacingRightValue);
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
        baseAttackPowerFactor *= attackPowerFactor;
        this.attackPowerFactor = baseAttackPowerFactor;
    }

    internal void UpdateByState(State state)
    {
        this.state = state;
        
        attackPowerFactor = baseAttackPowerFactor * state.attackPowerModifier * buff.attackBuffFactor;
    }
}
