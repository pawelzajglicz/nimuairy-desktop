using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWallDefender : WallDefenderAction
{
    [SerializeField] FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] FieldDefenderAttacking fieldDefenderAttacking;

    [SerializeField] public float attackBuffFactor = 1.3f;
    [SerializeField] public float speedBuffFactor = 1.25f;
    [SerializeField] public float buffTime = 2.75f;

    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
        fieldDefenderAttacking = FindObjectOfType<FieldDefenderAttacking>();

        StartCoroutine(ProcessBuff());
    }

    private IEnumerator ProcessBuff()
    {
        GiveBuff();
        yield return new WaitForSeconds(buffTime);
        TakeBuff();

        Destroy(gameObject);
    }

    private void GiveBuff()
    {
        fieldDefenderMovement.ModifyAccelerationByFactor(speedBuffFactor * factorFromWallDefender);
        fieldDefenderMovement.ModifyMaxHorizontalSpeedByFactor(speedBuffFactor * factorFromWallDefender);
        fieldDefenderMovement.ModifyMaxVerticalSpeedByFactor(speedBuffFactor * factorFromWallDefender);

        fieldDefenderAttacking.ModifyAttackPowerFactorByFactor(attackBuffFactor * factorFromWallDefender);
    }

    private void TakeBuff()
    {
        fieldDefenderMovement.ModifyAccelerationByFactor(1 / (speedBuffFactor * factorFromWallDefender));
        fieldDefenderMovement.ModifyMaxHorizontalSpeedByFactor(1 / (speedBuffFactor * factorFromWallDefender));
        fieldDefenderMovement.ModifyMaxVerticalSpeedByFactor(1 / (speedBuffFactor * factorFromWallDefender));

        fieldDefenderAttacking.ModifyAttackPowerFactorByFactor(1 / (attackBuffFactor * factorFromWallDefender));
    }
}
