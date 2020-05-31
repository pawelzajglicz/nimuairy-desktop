using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{

    [SerializeField] public float startSpeed = 0.5f;
    [SerializeField] public float currentSpeed = 0.5f;
    [SerializeField] public bool isStandardMoveAllowed;
    [SerializeField] protected bool arrivedAtWall;
    
    [SerializeField] protected EnemyTimeManagerReacting enemyTimeManagerReacting;

    protected Animator animator;


    void Start()
    {
        isStandardMoveAllowed = true;
        currentSpeed = startSpeed;

        enemyTimeManagerReacting = GetComponent<EnemyTimeManagerReacting>();
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    void Update()
    {

        if (isStandardMoveAllowed && /*!arrivedAtWall &&*/ FindObjectOfType<GameManager>().IsBattle)
        {
            ProcessStandardMove();
            animator.speed = 1;
        }

        if (enemyTimeManagerReacting.isReactingToFieldDefenderTimeFactor)
        {
            currentSpeed = startSpeed * TimeManager.playerTimeFactor;
            animator.speed = TimeManager.playerTimeFactor;
        }
    }

    protected abstract void ProcessStandardMove();

    public virtual void SetStandardMoveAllowed(bool newValue)
    {
        isStandardMoveAllowed = newValue;
    }

    public virtual void DontMoveInStandardWay()
    {
        if (isStandardMoveAllowed)
        {
            isStandardMoveAllowed = false;
        }
    }

    public virtual void StartMoveInStandardWay()
    {
        if (!isStandardMoveAllowed)
        {
            isStandardMoveAllowed = true;
        }
    }
}
