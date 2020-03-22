using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldAttackManager : MonoBehaviour
{
    // TODO: Refactor

    [SerializeField] public AttackFieldDefender LastQuickAttack { get; set; }
    [SerializeField] public AttackFieldDefender LastSlowAttack { get; set; }
    [SerializeField] List<AttackFieldDefender> lastAttacks;

    [SerializeField] public float timeToBreakCombo = 1f;
    [SerializeField] public float timeFromLastAttack;
    
    FieldDefenderMovement fieldDefenderMovement;

    [SerializeField] public ComboAttack pushAwayComboAttack;



    private void Awake()
    {
        /*lastAttacks = new FixedSizedQueue<AttackFieldDefender>();
        lastAttacks.Limit = 3;*/
        lastAttacks = new List<AttackFieldDefender>();
        fieldDefenderMovement = GetComponent<FieldDefenderMovement>();
    }

    private void Update()
    {
        timeFromLastAttack += Time.deltaTime;
        if (timeFromLastAttack > timeToBreakCombo)
        {
            lastAttacks.Clear();
        }
    }

    internal void PushQuickAttack(AttackFieldDefender attackInstance)
    {
        LastQuickAttack = attackInstance;
        AddAttackToComboList(attackInstance);
    }

    internal void PushSlowAttack(AttackFieldDefender attackInstance)
    {
        LastSlowAttack = attackInstance;
        AddAttackToComboList(attackInstance);
    }

    private void AddAttackToComboList(AttackFieldDefender attackInstance)
    {
        timeFromLastAttack = 0f;
        lastAttacks.Add(attackInstance);
        CheckForComboAttacks();
    }

    private void CheckForComboAttacks()
    {
        TryForPushAwayComboAttack();
    }

    private void TryForPushAwayComboAttack()
    {
        int lastAttacksNumber = lastAttacks.Count;
        if (lastAttacks.Count >=3 && lastAttacks[lastAttacksNumber - 3].GetComponent<QuickAttack>() && lastAttacks[lastAttacksNumber - 2].GetComponent<QuickAttack>() && lastAttacks[lastAttacksNumber - 1].GetComponent<SlowAttack>())
        {
            lastAttacks.Clear();
            Instantiate(pushAwayComboAttack, transform.position, Quaternion.identity);
        }

    }
}
