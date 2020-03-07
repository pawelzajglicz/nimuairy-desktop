using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayComboAttack : ComboAttack
{
    [SerializeField] public float pushDistance = 3.5f;
    [SerializeField] public float pushDuration = 0.4f;

    protected override void DealDamageToEnemy(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(attackPower);
        }

        StartCoroutine(PushEnemy(collision.GetComponent<Enemy>()));
    }
    

    public IEnumerator PushEnemy(Enemy enemyToPush)
    {
        float elapsedTime = 0;
        Vector2 start = enemyToPush.transform.position;
        
        Vector2 direction = enemyToPush.transform.position - transform.position;

        if (direction == Vector2.zero)
        {
            direction = Vector3.up;
        }

        Vector2 destination = (Vector2)transform.position + (direction * pushDistance);

        while (elapsedTime < pushDuration)
        {
            enemyToPush.transform.position = Vector2.Lerp(start, destination, (elapsedTime / pushDuration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
