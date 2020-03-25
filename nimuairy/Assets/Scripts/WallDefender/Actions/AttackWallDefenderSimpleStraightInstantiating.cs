using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraightInstantiating : AttackWallDefenderSimpleStraight
{
        [SerializeField] GameObject objectToInstantiate;
    

    protected override void TargetAchievedAction()
    {
        Instantiate(objectToInstantiate, targetPosition, Quaternion.identity);
        Destroy(gameObject);
    }

   
}
