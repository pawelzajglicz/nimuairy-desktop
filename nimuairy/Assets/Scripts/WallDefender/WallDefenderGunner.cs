using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderGunner : WallDefender
{
    
    protected override void InstantiateSlowAttack()
    {

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            return;
        }

        Debug.Log(enemies.Length);
        foreach (Enemy enemy in enemies)
        {
            WallDefenderAction fastAttackInstance = Instantiate(fastAction, transform.position, Quaternion.identity) as WallDefenderAction;
            fastAttackInstance.SetTarget(enemy.transform.position);
            Debug.Log("a");
        }

    }
}
