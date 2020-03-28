using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic : MonoBehaviour
{
    [SerializeField] public float poisonAttackFactor = 0.995f;
    [SerializeField] public float poisonSpeedFactor = 0.995f;
    [SerializeField] public float poisonInterval = 0.4f;
    [SerializeField] public float timeSinceLastPoisonEffect;
    [SerializeField] public Toxic toxic;

    [SerializeField] public EnemyAttacking enemyAttacking;
    [SerializeField] public EnemyMovement enemyMovement;
    public Enemy enemyToToxic;
    
    private void Update()
    {
        timeSinceLastPoisonEffect += (Time.deltaTime * TimeManager.playerTimeFactor);

        if (timeSinceLastPoisonEffect > poisonInterval)
        {
            timeSinceLastPoisonEffect = 0.0f;
            AffectEnemy();
        }
    }

    private void AffectEnemy()
    {
        enemyAttacking.attackPowerFactor *= poisonAttackFactor;
        enemyMovement.currentSpeed *= poisonSpeedFactor;
    }

    internal void poison(Enemy enemy)
    {
        enemyToToxic = enemy;

        foreach (Transform child in enemy.transform)
        {
            EnemyAttacking childEnemyAttacking = child.transform.gameObject.GetComponent<EnemyAttacking>();
            if (childEnemyAttacking)
            {
                enemyAttacking = childEnemyAttacking;
                break;
            }
        }

        enemyMovement = enemy.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && IsNotPoisoned(enemy))
        {
            Poison(enemy);
        }
    }

    private void Poison(Enemy enemy)
    {
        Toxic toxicInstance = Instantiate(toxic, enemy.transform.position, Quaternion.identity) as Toxic;
        toxic.poison(enemy);
        toxicInstance.transform.parent = enemy.transform;
    }

    private bool IsNotPoisoned(Enemy enemy)
    {
        foreach (Transform child in enemy.transform)
        {
            Toxic enemyToxic = child.transform.gameObject.GetComponent<Toxic>();
            if (enemyToxic)
            {
                return false;
            }
        }
        return true;
    }
}
