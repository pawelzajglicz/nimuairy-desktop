using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderParameters : MonoBehaviour
{

    [SerializeField] float attackPower = 10f;
    [SerializeField] float fieldDefenderDamagingFactor = 0.1f;

    public float AttackPower => attackPower;
    public float FieldDefenderDamagingFactor => fieldDefenderDamagingFactor;
}
