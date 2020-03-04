using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifier : MonoBehaviour
{

    [SerializeField] public bool isModifierActive;

    public bool IsModifierActive()
    {
        return isModifierActive;
    }
}
