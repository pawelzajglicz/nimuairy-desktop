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


    void Start()
    {
        isStandardMoveAllowed = true;
        currentSpeed = startSpeed;

        enemyTimeManagerReacting = GetComponent<EnemyTimeManagerReacting>();
    }

    void Update()
    {
        if (enemyTimeManagerReacting.isReactingToFieldDefenderTimeFactor)
        {
            currentSpeed = startSpeed * TimeManager.playerTimeFactor;
        }

        if (isStandardMoveAllowed && !arrivedAtWall && FindObjectOfType<GameManager>().IsBattle)
        {
            ProcessStandardMove();
        }
    }

    protected abstract void ProcessStandardMove();

    public virtual void SetStandardMoveAllowed(bool newValue)
    {
        isStandardMoveAllowed = newValue;
    }

    public virtual void DontMove()
    {
        if (isStandardMoveAllowed)
        {
            isStandardMoveAllowed = false;
        }
    }

    public virtual void StartMove()
    {
        if (!isStandardMoveAllowed)
        {
            isStandardMoveAllowed = true;
        }
    }
}
