using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderWizardLightning : TargetFinder
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

        Enemy biggestEnemy = FindBiggestEnemy(enemies);

        return biggestEnemy.gameObject;
    }

    private Enemy FindBiggestEnemy(Enemy[] enemies)
    {
        Enemy biggestEnemy = enemies[0];
        foreach (Enemy enemy in enemies)
        {
            if (enemy.biggerity > biggestEnemy.biggerity)
            {
                biggestEnemy = enemy;
            }
        }

        return biggestEnemy;
    }
}
