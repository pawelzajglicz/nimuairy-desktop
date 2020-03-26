using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFozening : MonoBehaviour
{

    private float enemySpeedBeforeFreezen;

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
        enemyToFrozen.GetComponent<EnemyMovement>().currentSpeed = 0;
    }

    private void OnDestroy()
    {
        enemyToFrozen.GetComponent<EnemyMovement>().currentSpeed = enemySpeedBeforeFreezen;
    }
}
