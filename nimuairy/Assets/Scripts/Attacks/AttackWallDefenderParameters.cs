using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderParameters : MonoBehaviour
{

    [SerializeField] float attackPower = 10f;

    public float GetAttackPower()
    {
        return attackPower;
    }
}
