using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefender : MonoBehaviour
{
    [SerializeField] AttackWallDefender fastAttack;
    [SerializeField] AttackWallDefender slowAttack;

    [SerializeField] float fastAttackInterval = 1f;
    [SerializeField] float slowAttackInterval = 6f;

    [SerializeField] float timeToFastAttack;
    [SerializeField] float timeToSlowAttack;

    TargetFinder targetFinder;

    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToFastAttack = fastAttackInterval;
        timeToSlowAttack = slowAttackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttacks();
    }

    private void HandleAttacks()
    {
        HandleFastAttack();
        HandleSlowAttack();
    }

    private void HandleFastAttack()
    {
        timeToFastAttack -= Time.deltaTime;

        if (timeToFastAttack < 0)
        {
            timeToFastAttack = fastAttackInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateFastAttack();
            }
        }
    }

    private void InstantiateFastAttack()
    {
        AttackWallDefender fastAttackInstance = Instantiate(fastAttack, transform.position, transform.rotation) as AttackWallDefender;
        fastAttackInstance.SetTarget(targetFinder.FindTarget());
    }

    private void HandleSlowAttack()
    {
        timeToSlowAttack -= Time.deltaTime;

        if (timeToSlowAttack < 0)
        {
            timeToSlowAttack = slowAttackInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateSlowAttack();
            }
        }
    }

    private void InstantiateSlowAttack()
    {
        AttackWallDefender slowAttackInstance = Instantiate(slowAttack, transform.position, transform.rotation) as AttackWallDefender;
        slowAttackInstance.SetTarget(targetFinder.FindTarget());
    }
}
