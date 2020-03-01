using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderWizardLightning : TargetFinder
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector2 FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return GetDefaultTarget();
        }

        Enemy biggestEnemy = FindBiggestEnemy(enemies);

        return biggestEnemy.transform.position;
    }

    private static Enemy FindBiggestEnemy(Enemy[] enemies)
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
