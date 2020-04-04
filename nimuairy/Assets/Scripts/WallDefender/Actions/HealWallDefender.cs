using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealWallDefender : WallDefenderAction
{
    [SerializeField] public float healPoints = 25f;
    [SerializeField] public float lifeTime = 0.5f;

    private void Start()
    {
        Health targetHealth = targetGameObject.GetComponent<Health>();
        TryHeal(targetHealth);
        Destroy(gameObject, lifeTime);
    }

    private void TryHeal(Health targetHealth)
    {
        if (targetHealth)
        {
            targetHealth.Heal(healPoints * factorFromWallDefender));
        }
    }
}
