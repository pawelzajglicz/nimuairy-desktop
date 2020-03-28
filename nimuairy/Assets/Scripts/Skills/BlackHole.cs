using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    [SerializeField] public float duration = 1.25f;
    [SerializeField] public float elapsedTime;
    [SerializeField] public float pullingForceValue = 0.01f;


    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        PullEnemies();
    }

    private void PullEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                PullEnemy(enemy);
            }
        }

        elapsedTime += (Time.deltaTime * Time.deltaTime);
    }

    private void PullEnemy(Enemy enemy)
    {

        Vector2 direction = (transform.position - enemy.transform.position).normalized;

        if (direction == Vector2.zero)
        {
            return;
        }
		
        enemy.transform.position += (Vector3)(direction * pullingForceValue);
    }
}
