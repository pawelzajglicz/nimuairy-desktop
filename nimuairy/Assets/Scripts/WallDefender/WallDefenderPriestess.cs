using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderPriestess : WallDefender
{
    [SerializeField] GameObject fieldDefenderGameObject;

    protected override void Start()
    {
        base.Start();
        fieldDefenderGameObject = FindObjectOfType<FieldDefenderMovement>().gameObject;
    }


    protected override void InstantiateFastAttack()
    {
        GameObject target = targetFinder.FindTarget();
        WallDefenderAction fastActionInstance = Instantiate(fastAction, target.transform.position, Quaternion.identity) as WallDefenderAction;
        fastActionInstance.factorFromWallDefender = actionFactor;
        fastActionInstance.transform.parent = target.transform;
        fastActionInstance.SetTarget(target);
    }

    protected override void InstantiateSlowAttack()
    {
        WallDefenderAction slowActionInstance = Instantiate(slowAction, fieldDefenderGameObject.transform.position, Quaternion.identity) as WallDefenderAction;
        slowActionInstance.factorFromWallDefender = actionFactor;
        slowActionInstance.transform.parent = fieldDefenderGameObject.transform;
        slowActionInstance.SetTarget(fieldDefenderGameObject);
    }
}
