using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderArcher: TargetFinder
{


    public override Vector2 FindTargetPosition()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return GetDefaultTarget();
        }

        Enemy closestToWallEnemy = FindClosestToWallEnemy(enemies);

        return closestToWallEnemy.transform.position;
    }

    public override GameObject FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return null;
        }

        Enemy closestToWallEnemy = FindClosestToWallEnemy(enemies);

        return closestToWallEnemy.gameObject;
    }

    private Enemy FindClosestToWallEnemy(Enemy[] enemies)
    {
        Enemy closestEnemy = enemies[0];
        foreach (Enemy enemy in enemies)
        {
            if (enemy.transform.position.x < closestEnemy.transform.position.x)
            {
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
