using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] State attackState;
    [SerializeField] State defenceState;

    [SerializeField] State currentState;
    [SerializeField] State currentStateInstance;

    [SerializeField] FieldDefenderMovement fieldDefender;

    private void Awake()
    {
        currentState = defenceState;
        InstantiateCurrentState();
        fieldDefender = GetComponent<FieldDefenderMovement>();
    }

    private void Update()
    {
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        ChangeToNextState();
        InstantiateCurrentState();
        fieldDefender.SetState(currentStateInstance);
    }

    private void InstantiateCurrentState()
    {
        currentStateInstance = Instantiate(currentState, transform.position, Quaternion.identity);
        currentStateInstance.transform.parent = transform;
    }

    private void ChangeToNextState()
    {
        if (currentState == attackState)
        {
            Destroy(currentStateInstance.gameObject);
            currentState = defenceState;
        }
        else
        {
            Destroy(currentStateInstance.gameObject);
            currentState = attackState;
        }
    }

    public float GetAttackPowerModifier()
    {
        return currentState.GetAttackPowerModifier();
    }

    public float GetSpeedModifier()
    {
        return currentState.GetSpeedModifier();
    }

    public State getCurrentState()
    {
        return currentState;
    }

    public bool IsInAttackingState()
    {
        return currentState == attackState;
    }

    public bool IsInDefenceState()
    {
        return currentState == defenceState;
    }
}
