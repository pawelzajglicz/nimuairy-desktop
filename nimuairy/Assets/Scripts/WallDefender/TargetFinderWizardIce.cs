using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderWizardIce : TargetFinder
{
    public override Vector2 FindTargetPosition()
    {
        return FindTarget().transform.position;
    }

    public override GameObject FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return null;
        }

        Enemy closestToWallEnemy = FindFarestToWallEnemy(enemies);

        return closestToWallEnemy.gameObject;
    }

    private Enemy FindFarestToWallEnemy(Enemy[] enemies)
    {
        Enemy closestEnemy = enemies[0];
        foreach (Enemy enemy in enemies)
        {
            if (enemy.transform.position.x > closestEnemy.transform.position.x)
            {
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
