using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFozening : MonoBehaviour
{

    private float enemySpeedBeforeFreezen;
    private float enemyStartSpeedBeforeFreezen;

    [SerializeField] public float lifeTime = 3f;

    [SerializeField] Enemy enemyToFrozen;
    

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    internal void freeze(Enemy enemy)
    {
        enemyToFrozen = enemy;
        enemySpeedBeforeFreezen = enemyToFrozen.GetComponent<EnemyMovement>().currentSpeed;
        enemyStartSpeedBeforeFreezen = enemyToFrozen.GetComponent<EnemyMovement>().startSpeed;
        enemyToFrozen.GetComponent<EnemyMovement>().currentSpeed = 0;
        enemyToFrozen.GetComponent<EnemyMovement>().startSpeed = 0;
    }

    private void OnDestroy()
    {
        if (enemyToFrozen)
        {
            enemyToFrozen.GetComponent<EnemyMovement>().currentSpeed = enemySpeedBeforeFreezen;
            enemyToFrozen.GetComponent<EnemyMovement>().startSpeed = enemyStartSpeedBeforeFreezen;
        }
    }
}
