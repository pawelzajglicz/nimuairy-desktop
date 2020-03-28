﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallActionAtColliding : WallDefenderActionAtColliding
{
    [SerializeField] EnemyFozening enemyFozening;

    protected override void PerformAction(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy && IsNotFreezed(enemy))
        {
            EnemyFozening frozeningInstance = Instantiate(enemyFozening, collision.transform.position, Quaternion.identity) as EnemyFozening;
            frozeningInstance.freeze(enemy);
        }
    }

    private bool IsNotFreezed(Enemy enemy)
    {
        foreach (Transform child in enemy.transform)
        {
            EnemyFozening enemyFreezing = child.transform.gameObject.GetComponent<EnemyFozening>();
            if (enemyFreezing)
            {
                return false;
            }
        }
        return true;
    }
}