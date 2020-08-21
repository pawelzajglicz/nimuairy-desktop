using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWallDefender : WallDefenderAction
{
    [SerializeField] FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] FieldDefenderAttacking fieldDefenderAttacking;

    [SerializeField] public float attackBuffFactor = 1.3f;
    [SerializeField] public float baseAttackBuffFactor = 1.3f;
    [SerializeField] public float baseSpeedBuffFactor = 1.25f;
    [SerializeField] public float speedBuffFactor = 1.25f;
    [SerializeField] public float buffTime = 2.75f;
    [SerializeField] public float liveTime = 0f;

    [SerializeField] public float buffAgainstHealingFactor = 0.2f;

    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
        fieldDefenderAttacking = FindObjectOfType<FieldDefenderAttacking>();

        TakeBuff();
    }

    private void Update()
    {
        liveTime += Time.deltaTime;
        if (liveTime > buffTime)
        {
            TakeBuff();
            Destroy(gameObject);
        }
    }

    private void GiveBuff()
    {
        speedBuffFactor = baseSpeedBuffFactor * (1 + factorFromWallDefender * buffAgainstHealingFactor);
        fieldDefenderMovement.SetBuff(this);
        fieldDefenderAttacking.SetBuff(this);
    }

    private void TakeBuff()
    {
        attackBuffFactor = baseAttackBuffFactor * (1 + factorFromWallDefender * buffAgainstHealingFactor);

        fieldDefenderMovement.TakeBuff(this);
        fieldDefenderAttacking.TakeBuff(this);
    }
}
