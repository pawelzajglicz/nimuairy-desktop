using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    [SerializeField] public float lifeTime = 3f;
    [SerializeField] public float burnDamage = 22f;
    [SerializeField] public float burnInterval = 0.4f;
    [SerializeField] public float timeSinceLastBurnDamage;
    [SerializeField] public Burning burning;

    public Enemy enemyToBurn;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        timeSinceLastBurnDamage += (Time.deltaTime/* * TimeManager.playerTimeFactor*/);

        if (timeSinceLastBurnDamage > burnInterval)
        {
            timeSinceLastBurnDamage = 0.0f;
            DamageEnemy();
        }
    }

    private void DamageEnemy()
    {
        Health enemyHealth = enemyToBurn.GetComponent<Health>();
        if (enemyHealth)
        {
            enemyHealth.DealDamage(burnDamage);
        }
    }

    internal void burn(Enemy enemy)
    {
        enemyToBurn = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && IsNotBurning(enemy))
        {
            SetFireTo(enemy);
        }
    }

    private void SetFireTo(Enemy enemy)
    {
        Burning burningInstance = Instantiate(burning, enemy.transform.position, Quaternion.identity) as Burning;
        burningInstance.burn(enemy);
        burningInstance.transform.parent = enemy.transform;
    }

    private bool IsNotBurning(Enemy enemy)
    {
        foreach (Transform child in enemy.transform)
        {
            Burning enemyBurning = child.transform.gameObject.GetComponent<Burning>();
            if (enemyBurning)
            {
                return false;
            }
        }
        return true;
    }
}
