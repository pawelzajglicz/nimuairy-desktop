using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefender : MonoBehaviour
{
    [SerializeField] protected WallDefenderAction fastAction;
    [SerializeField] protected WallDefenderAction slowAction;

    [SerializeField] protected float fastActionInterval = 1f;
    [SerializeField] protected float slowActionInterval = 6f;

    [SerializeField] protected float timeToFastAction;
    [SerializeField] protected float timeToSlowAction;

    protected TargetFinder targetFinder;

    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
    }
    
    protected virtual void Start()
    {
        timeToFastAction = fastActionInterval;
        timeToSlowAction = slowActionInterval;
    }
    
    void Update()
    {
        HandleAttacks();
    }

    private void HandleAttacks()
    {
        HandleFastAttack();
        HandleSlowAttack();
    }

    protected virtual void HandleFastAttack()
    {
        timeToFastAction -= Time.deltaTime;

        if (timeToFastAction < 0)
        {
            timeToFastAction = fastActionInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateFastAttack();
            }
        }
    }

    protected virtual void InstantiateFastAttack()
    {
        WallDefenderAction fastAttackInstance = Instantiate(fastAction, transform.position, transform.rotation) as WallDefenderAction;
        fastAttackInstance.SetTarget(targetFinder.FindTarget());
    }

    protected virtual void HandleSlowAttack()
    {
        timeToSlowAction -= Time.deltaTime;

        if (timeToSlowAction < 0)
        {
            timeToSlowAction = slowActionInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateSlowAttack();
            }
        }
    }

    protected virtual void InstantiateSlowAttack()
    {
        WallDefenderAction slowAttackInstance = Instantiate(slowAction, transform.position, transform.rotation) as WallDefenderAction;
        slowAttackInstance.SetTarget(targetFinder.FindTarget());
    }
}
