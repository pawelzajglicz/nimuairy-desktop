using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] EnemyAttack attack;
    [SerializeField] float attackTimeRate = 1.4f;

    private HashSet<GameObject> objectsToDealDamage;

    private void Awake()
    {
        objectsToDealDamage = new HashSet<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (IsGameObjectAttackable(collided))
        {
            Attack(collided);
        }
    }

    private static bool IsGameObjectAttackable(GameObject collided)
    {
        return collided.CompareTag("Wall") || collided.CompareTag("FieldDefender");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (IsGameObjectAttackable(collided))
        {
            StopDealDamageToGameObject(collided);
        }
    }

    private void Attack(GameObject gameObjectToGetDamage)
    {
        MakeAttack(gameObjectToGetDamage);
        StartCoroutine(ProcessAttacking(gameObjectToGetDamage));
    }

    private void MakeAttack(GameObject gameObjectToGetDamage)
    {
        InstantiateAttack();
        DealDamage(gameObjectToGetDamage);
    }

    private void InstantiateAttack()
    {
        EnemyAttack attackInstance = Instantiate(attack, transform.position, Quaternion.identity) as EnemyAttack;
        attackInstance.transform.parent = transform;
    }

    private void DealDamage(GameObject gameObjectToGetDamage)
    {
        Health health = gameObjectToGetDamage.GetComponent<Health>();
        if (health)
        {
            objectsToDealDamage.Add(gameObjectToGetDamage);
            health.DealDamage(attack.GetAttackPower());
        }
    }

    IEnumerator ProcessAttacking(GameObject gameObjectToGetDamage)
    {
        yield return new WaitForSeconds(attackTimeRate);

        if (objectsToDealDamage.Contains(gameObjectToGetDamage))
        {
            Attack(gameObjectToGetDamage);
        }
    }

    private void StopDealDamageToGameObject(GameObject gameObjectToStopGetDamage)
    {
        if (objectsToDealDamage.Contains(gameObjectToStopGetDamage))
        {
            objectsToDealDamage.Remove(gameObjectToStopGetDamage);
        }
    }
}
