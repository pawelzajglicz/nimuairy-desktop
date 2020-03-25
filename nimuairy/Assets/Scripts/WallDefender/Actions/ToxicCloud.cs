using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicCloud : MonoBehaviour
{
    [SerializeField] public float lifeTime = 0.5f;
    [SerializeField] public Toxic toxic;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && IsNotPoisoned(enemy))
        {
            Poison(enemy);
        }
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

    private void Poison(Enemy enemy)
    {
        Toxic toxicInstance = Instantiate(toxic, enemy.transform.position, Quaternion.identity) as Toxic;
        toxic.poison(enemy);
        toxicInstance.transform.parent = enemy.transform;
    }
}
