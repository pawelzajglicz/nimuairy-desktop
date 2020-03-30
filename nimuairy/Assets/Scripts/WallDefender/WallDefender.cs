using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefender : MonoBehaviour
{
    [SerializeField] protected WallDefenderAction fastAction;
    [SerializeField] protected WallDefenderAction slowAction;
    [SerializeField] public WallDefenderSlot slot;
    [SerializeField] public WallDefenderPlaceHolder placeholder;

    public bool isManualTargeting;

    [SerializeField] public float fastActionInterval = 1f;
    [SerializeField] public float slowActionInterval = 6f;

    [SerializeField] public float timeToFastAction;
    [SerializeField] public float timeToSlowAction;

    [SerializeField] public bool isActive;

    

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
        if (isActive)
        {
            HandleAttacks();
        }
    }

    private void HandleAttacks()
    {
        HandleFastAttack();
        HandleSlowAttack();
    }

    protected virtual void HandleFastAttack()
    {
        timeToFastAction -= Time.deltaTime * TimeManager.playerTimeFactor;

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
        timeToSlowAction -= Time.deltaTime * TimeManager.playerTimeFactor;

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

        if (isManualTargeting)
        {
            slowAttackInstance.SetTarget(slot.GetTargetPosition());
        }
        else
        {
            slowAttackInstance.SetTarget(targetFinder.FindTarget());
        }
    }

    public void ReturnToPlaceholder()
    {
        Debug.Log("dsds");
        transform.position = placeholder.transform.position;
        isActive = false;
        slot.wallDefender = null;
        slot = null;
    }
}
