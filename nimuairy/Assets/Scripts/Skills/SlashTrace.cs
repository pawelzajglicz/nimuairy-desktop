using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTrace : MonoBehaviour
{
    [SerializeField] public float attackPower = 35f;
    [SerializeField] public float lifeTime = 1f;
    [SerializeField] float traceCollidingTime = 0.3f;

    private void Start()
    {
        StartCoroutine(TurnOffColliding());
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            DealDamageToEnemy(collision);
        }
    }

    protected virtual void DealDamageToEnemy(Collision2D collision)
    {
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackPower);
        }
    }

    private IEnumerator TurnOffColliding()
    {
        yield return new WaitForSeconds(traceCollidingTime);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }
}
