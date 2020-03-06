using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderPriestess : WallDefender
{
    [SerializeField] GameObject fieldDefenderGameObject;

    private void Start()
    {
        fieldDefenderGameObject = FindObjectOfType<FieldDefenderMovement>().gameObject;
    }


    protected override void InstantiateFastAttack()
    {
        GameObject target = targetFinder.FindTarget();
        WallDefenderAction fastActionInstance = Instantiate(fastAction, target.transform.position, Quaternion.identity) as WallDefenderAction;
        fastActionInstance.transform.parent = target.transform;
        fastActionInstance.SetTarget(target);
    }

    protected override void InstantiateSlowAttack()
    {
        WallDefenderAction slowActionInstance = Instantiate(slowAction, fieldDefenderGameObject.transform.position, Quaternion.identity) as WallDefenderAction;
        slowActionInstance.transform.parent = fieldDefenderGameObject.transform;
        slowActionInstance.SetTarget(fieldDefenderGameObject);
    }
}
