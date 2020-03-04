using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] public float attackPowerModifier = 1.2f;
    [SerializeField] public float speedModifier = 1.3f;

   
    public float GetAttackPowerModifier()
    {
        return attackPowerModifier;
    }

    public float GetSpeedModifier()
    {
        return speedModifier;
    }
}
