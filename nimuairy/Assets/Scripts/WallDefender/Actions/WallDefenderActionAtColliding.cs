using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallDefenderActionAtColliding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PerformAction(collision);
    }

    protected abstract void PerformAction(Collider2D collision);
}
