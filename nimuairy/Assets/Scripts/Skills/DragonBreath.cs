using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    [SerializeField] public float lifeTime = 0.5f;
    [SerializeField] public Burning burning;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && IsNotBurning(enemy))
        {
            SetFireTo(enemy);
        }
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

    private void SetFireTo(Enemy enemy)
    {
        Burning burningInstance = Instantiate(burning, enemy.transform.position, Quaternion.identity) as Burning;
        burning.burn(enemy);
        burningInstance.transform.parent = enemy.transform;
    }
}
