using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderChemist : TargetFinder
{
    [SerializeField] FieldDefenderMovement fieldDefender;


    protected override void Start()
    {
        base.Start();
        fieldDefender = FindObjectOfType<FieldDefenderMovement>();
    }

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

        Enemy closestToWallEnemy = FindSmallestEnemy(enemies);

        return closestToWallEnemy.gameObject;
    }

    private Enemy FindSmallestEnemy(Enemy[] enemies)
    {
        Enemy smallestEnemy = enemies[0];
        foreach (Enemy enemy in enemies)
        {
            if (enemy.biggerity < smallestEnemy.biggerity)
            {
                smallestEnemy = enemy;
            }
        }

        return smallestEnemy;
    }
}
