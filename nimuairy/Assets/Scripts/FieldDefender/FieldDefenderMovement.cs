﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDefenderMovement : Paramizable
{
    private float playerTimeFactorModifier = 1.2f;
    [SerializeField] public float timeFactor = 0;

    [SerializeField] float speedHorizontal = 0;
    [SerializeField] float speedVertical = 0;
    [SerializeField] float maxHorizontalSpeed = 10;
    [SerializeField] Param speedParam;
    [SerializeField] float maxVerticalSpeed;// = 8;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    [SerializeField] float baseMaxHorizontalSpeed;
    [SerializeField] float baseMaxVerticalSpeed;
    [SerializeField] float baseAcceleration = 10;
    [SerializeField] float baseDeceleration = 10;

    StateManager stateManager;
    [SerializeField] State state;

    public Vector2 direction;
    BuffWallDefender buff;

    public State State => state;
    public int FacingRightValue => facingRight;
    GameManager gameManager;
    [SerializeField] FieldDefenderAttacking fieldDefenderAttacking;

    int facingRight = 1;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        stateManager = GetComponent<StateManager>();
        if (stateManager)
        {
            state = stateManager.getCurrentState();
        }

        UpdateParams();
        UpdateSpeed();
    }

    void Update()
    {
        if (gameManager.IsBattle)
        {
            HandleMoving();
        }
    }

    public override void UpdateParams()
    {
        baseMaxHorizontalSpeed = speedParam.ParamValue * 0.1f;
        baseAcceleration = speedParam.ParamValue * 0.1f;
        baseDeceleration = speedParam.ParamValue * 0.1f;
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        baseMaxVerticalSpeed = baseMaxHorizontalSpeed * 0.8f;
        CalculateParams();
    }

    private void HandleMoving()
    {
        CalculatingHorizontalSpeed();
        CalculatingVerticalSpeed();

        Vector2 vectorOffset = new Vector2(speedHorizontal * Time.deltaTime, speedVertical * Time.deltaTime);
        direction = vectorOffset.normalized;
        transform.position += (Vector3)vectorOffset;
        HandleFacingDirection();
        UpdateTimeFactor();
    }

    internal void ResetSpeed()
    {
        speedHorizontal = 0f;
        speedVertical = 0f;
        UpdateSpeed();
    }

    // TODO: refactoring
    private void CalculatingHorizontalSpeed()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && (speedHorizontal > -maxHorizontalSpeed))
        {
            speedHorizontal = speedHorizontal - (acceleration * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (speedHorizontal < maxHorizontalSpeed))
        {
            speedHorizontal = speedHorizontal + (acceleration * Time.deltaTime);
        }
        else
        {
            if (speedHorizontal > deceleration * Time.deltaTime)
                speedHorizontal = speedHorizontal - deceleration * Time.deltaTime;
            else if (speedHorizontal < -deceleration * Time.deltaTime)
                speedHorizontal = speedHorizontal + deceleration * Time.deltaTime;
            else
                speedHorizontal = 0;
        }
    }

    private void CalculatingVerticalSpeed()
    {
        if (Input.GetKey(KeyCode.DownArrow) && (speedVertical > -maxVerticalSpeed))
        {
            speedVertical = speedVertical - acceleration * Time.deltaTime;
        }
        else if (!Input.GetKey(KeyCode.UpArrow) || speedVertical >= maxVerticalSpeed)
        {
            if (speedVertical > deceleration * Time.deltaTime)
                speedVertical = speedVertical - deceleration * Time.deltaTime;
            else if (speedVertical < -deceleration * Time.deltaTime)
                speedVertical = speedVertical + deceleration * Time.deltaTime;
            else
                speedVertical = 0;
        }
        else
        {
            speedVertical = speedVertical + acceleration * Time.deltaTime;
        }
    }

    private void HandleFacingDirection()
    {

        if (speedHorizontal < 0 && Mathf.Abs(speedHorizontal) > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            facingRight = -1;
        }
        else if (speedHorizontal > 0 && Mathf.Abs(speedHorizontal) > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            facingRight = 1;
        }
    }


    public void SetState(State state)
    {
        this.state = state;

        CalculateParams();
    }

    private void CalculateParams()
    {
        float buffSpeedFactor = 1f;
        if (buff != null) buffSpeedFactor = buff.speedBuffFactor;

        acceleration = baseAcceleration * state.GetSpeedModifier() * buffSpeedFactor;
        deceleration = baseDeceleration * state.GetSpeedModifier() * buffSpeedFactor;
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier() * buffSpeedFactor;
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier() * buffSpeedFactor;
    }

    internal void ModifyMaxVerticalSpeedByFactor(float speedBuffFactor)
    {
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier() * speedBuffFactor;
    }

    internal void ModifyMaxHorizontalSpeedByFactor(float speedBuffFactor)
    {
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier() * speedBuffFactor;
    }

    internal void ModifyAccelerationByFactor(float speedBuffFactor)
    {
        acceleration = baseAcceleration * state.GetSpeedModifier() * speedBuffFactor;
    }

    public float GetTimeFactor()
    {
        return timeFactor;
    }

    private void UpdateTimeFactor()
    {
        float horizontalTimeFactor = Mathf.Abs(speedHorizontal / maxHorizontalSpeed);
        float verticalTimeFactor = Mathf.Abs(speedVertical / maxVerticalSpeed);

        timeFactor = (horizontalTimeFactor + verticalTimeFactor) / 2 * playerTimeFactorModifier;

        TimeManager.playerTimeFactor = timeFactor;
    }

    internal void SetBuff(BuffWallDefender buffWallDefender)
    {
        buff = buffWallDefender;

        acceleration = baseAcceleration * state.GetSpeedModifier() * buff.speedBuffFactor;
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier() * buff.speedBuffFactor;
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier() * buff.speedBuffFactor;
    }

    internal void TakeBuff(BuffWallDefender buffWallDefender)
    {
        buff = null;

        acceleration = baseAcceleration * state.GetSpeedModifier();
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier();
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier();
    }


    private void OnDestroy()
    {
        FindObjectOfType<GameManager>().ProcessDefenceFailure();
    }
}
