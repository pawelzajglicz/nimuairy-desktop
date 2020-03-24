using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderGunner: TargetFinder
{
    System.Random random;

    private void Awake()
    {
        random = new System.Random();
    }

    public override Vector2 FindTargetPosition()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return GetDefaultTarget();
        }

        Enemy randomEnemy = enemies[random.Next(0, enemies.Length)];

        return randomEnemy.transform.position;
    }

    public override GameObject FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return null;
        }

        Enemy randomEnemy = enemies[random.Next(0, enemies.Length)];

        return randomEnemy.gameObject;
    }

 
}
