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

        Enemy closestToWallEnemy = FindClosestFieldDefender(enemies);

        return closestToWallEnemy.gameObject;
    }

    private Enemy FindClosestFieldDefender(Enemy[] enemies)
    {
        Enemy closestEnemy = enemies[0];
        float smallestDistance = Vector2.Distance(fieldDefender.transform.position, closestEnemy.transform.position);

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector2.Distance(fieldDefender.transform.position, enemy.transform.position);
            if (distance < smallestDistance)
            {
                closestEnemy = enemy;
                smallestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
