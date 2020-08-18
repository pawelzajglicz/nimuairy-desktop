using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] public EnemyAttack attack;
    [SerializeField] public EnemyMovement enemyMovement;

    [SerializeField] float attackTimeRate = 1.4f;
    [SerializeField] public float attackPowerFactor = 1.4f;

    [SerializeField] AudioClip attackSound;
    [SerializeField] float attackSoundVolume = 0.5f;

    Animator animator;

    private HashSet<GameObject> objectsToDealDamage;

    public EnemyAttack AttackPrefab { get => attack; set => attack = value; }

    private void Awake()
    {
        objectsToDealDamage = new HashSet<GameObject>();
        animator = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (IsGameObjectAttackable(collided) && FindObjectOfType<GameManager>().IsBattle)
        {
            AttackGameObject(collided);
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
            animator.SetBool("IsAttacking", false);
            enemyMovement.currentSpeed = enemyMovement.startSpeed;
        }
    }

    private void AttackGameObject(GameObject gameObjectToGetDamage)
    {
        MakeAttack(gameObjectToGetDamage);
        animator.SetBool("IsAttacking", true);
        enemyMovement.currentSpeed = 0f;
        StartCoroutine(ProcessAttacking(gameObjectToGetDamage));
    }

    private void MakeAttack(GameObject gameObjectToGetDamage)
    {
        InstantiateAttack();
        DealDamage(gameObjectToGetDamage);
        AudioSource.PlayClipAtPoint(attackSound, Camera.main.transform.position, attackSoundVolume);
    }

    private void InstantiateAttack()
    {
        EnemyAttack attackInstance = Instantiate(AttackPrefab, transform.position, Quaternion.identity) as EnemyAttack;
        attackInstance.attackPower *= attackPowerFactor;
        attackInstance.transform.parent = transform;
    }

    private void DealDamage(GameObject gameObjectToGetDamage)
    {
        Health health = gameObjectToGetDamage.GetComponent<Health>();
        if (health)
        {
            objectsToDealDamage.Add(gameObjectToGetDamage);
            health.DealDamage(AttackPrefab.GetAttackPower());
        }
    }

    IEnumerator ProcessAttacking(GameObject gameObjectToGetDamage)
    {
        yield return new WaitForSeconds(attackTimeRate);

        if (objectsToDealDamage.Contains(gameObjectToGetDamage))
        {
            AttackGameObject(gameObjectToGetDamage);
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
