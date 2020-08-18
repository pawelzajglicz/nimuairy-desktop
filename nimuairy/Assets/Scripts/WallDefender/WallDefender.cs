using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefender : Paramizable
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


    [SerializeField] protected float actionFactor = 1f;
    [SerializeField] Param actionFactorParam;

    [SerializeField] AudioClip fastActionSound;
    [SerializeField] float fastActionSoundVolume = 0.5f;

    [SerializeField] AudioClip slowActionSound;
    [SerializeField] float slowActionSoundVolume = 0.5f;


    Animator animator;



    protected TargetFinder targetFinder;

    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
        animator = GetComponent<Animator>();
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

    public override void UpdateParams()
    {
        if (actionFactorParam != null)
        {
            actionFactor = actionFactorParam.paramValue * 0.1f + 1;
        }
    }

    protected virtual void HandleFastAttack()
    {
        timeToFastAction -= Time.deltaTime * TimeManager.playerTimeFactor;

        if (timeToFastAction < 0)
        {
            //animator.SetBool("IsProcessingAction", true);
            animator.SetTrigger("ProcessAction");
            timeToFastAction = fastActionInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateFastAttack();
                AudioSource.PlayClipAtPoint(fastActionSound, Camera.main.transform.position, fastActionSoundVolume);
            }
            //animator.SetBool("IsProcessingAction", false);
        }
    }

    protected virtual void InstantiateFastAttack()
    {
        WallDefenderAction fastAttackInstance = Instantiate(fastAction, transform.position, transform.rotation) as WallDefenderAction;
        fastAttackInstance.setFactorFromWallDefender(actionFactorParam.paramValue);

        WallDefenderAction[] acs = fastAttackInstance.gameObject.GetComponents<WallDefenderAction>();

        foreach (WallDefenderAction action in acs)
        {
            action.setFactorFromWallDefender(actionFactorParam.paramValue);
        }

      //  fastAttackInstance.factorFromWallDefender = actionFactorParam.paramValue;
        fastAttackInstance.SetTarget(targetFinder.FindTarget());
    }

    protected virtual void HandleSlowAttack()
    {
        timeToSlowAction -= Time.deltaTime * TimeManager.playerTimeFactor;

        if (timeToSlowAction < 0)
        {
            animator.SetTrigger("ProcessAction");
            timeToSlowAction = slowActionInterval;
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                InstantiateSlowAttack();
                AudioSource.PlayClipAtPoint(slowActionSound, Camera.main.transform.position, slowActionSoundVolume);
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
        transform.position = placeholder.transform.position;
        transform.parent = placeholder.transform;
        isActive = false;
        slot.wallDefender = null;
        slot = null;
    }
}
