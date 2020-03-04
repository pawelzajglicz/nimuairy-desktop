using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] public float speed = 0.5f;
    [SerializeField] public bool isStandardMoveAllowed;

    public virtual void SetStandardMoveAllowed(bool newValue)
    {
        isStandardMoveAllowed = newValue;
    }

    public virtual void DontMove()
    {
        if (isStandardMoveAllowed)
        {
            isStandardMoveAllowed = false;
        }
    }

    public virtual void StartMove()
    {
        if (!isStandardMoveAllowed)
        {
            isStandardMoveAllowed = true;
        }
    }
}
