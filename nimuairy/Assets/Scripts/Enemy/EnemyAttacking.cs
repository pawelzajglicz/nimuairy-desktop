using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] public EnemyAttack attack;
    [SerializeField] public EnemyMovement enemyMovement;

    [SerializeField] float attackTimeRate = 1.4f;
    [SerializeField] public float attackPowerFactor = 1.4f;
    private float timeFromLastAttack = 0f;
    private bool processAttacking = false;

    [SerializeField] AudioClip attackSound;
    [SerializeField] float attackSoundVolume = 0.5f;

    Animator animator;
    [SerializeField] protected EnemyTimeManagerReacting enemyTimeManagerReacting;
    private float timeModifier = 1f;

    private HashSet<GameObject> objectsToDealDamage;

    public EnemyAttack AttackPrefab { get => attack; set => attack = value; }

    private void Awake()
    {
        objectsToDealDamage = new HashSet<GameObject>();
        animator = transform.parent.gameObject.GetComponent<Animator>();
        enemyTimeManagerReacting = transform.parent.GetComponent<EnemyTimeManagerReacting>();
    }

    private void Update()
    {
        if (enemyTimeManagerReacting.isReactingToFieldDefenderTimeFactor)
        {
            timeModifier = TimeManager.playerTimeFactor;
        }
        else
        {
            timeModifier = 1f;
        }

        if (processAttacking)
        {
;           timeFromLastAttack += Time.deltaTime * Mathf.Clamp01(timeModifier);
        }

        if (timeFromLastAttack > attackTimeRate)
        {
            AttackAllAllowedGameObjects();
            timeFromLastAttack = 0f;
        }
    }

    private void AttackAllAllowedGameObjects()
    {
        foreach (GameObject gameObject in objectsToDealDamage)
        {
            AttackGameObject(gameObject);
        }
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

        if (objectsToDealDamage.Count == 0)
        {
            timeFromLastAttack = 0f;
            processAttacking = false;
        }
    }

    private void AttackGameObject(GameObject gameObjectToGetDamage)
    {
        processAttacking = true;
        MakeAttack(gameObjectToGetDamage);
        animator.SetBool("IsAttacking", true);
        enemyMovement.currentSpeed = 0f;
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

    private void StopDealDamageToGameObject(GameObject gameObjectToStopGetDamage)
    {
        if (objectsToDealDamage.Contains(gameObjectToStopGetDamage))
        {
            objectsToDealDamage.Remove(gameObjectToStopGetDamage);
        }
    }
}
