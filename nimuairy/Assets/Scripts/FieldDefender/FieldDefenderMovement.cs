using System;
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
    [SerializeField] float acceleration = 10;
    [SerializeField] float deceleration = 10;

    [SerializeField] float baseMaxHorizontalSpeed;
    [SerializeField] float baseMaxVerticalSpeed;
    [SerializeField] float baseAcceleration;
    [SerializeField] float baseDeceleration;

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
        buff = EmptyBuff();

        UpdateParams();
        UpdateSpeed();

        stateManager = GetComponent<StateManager>();
        if (stateManager)
        {
            SetState(stateManager.getCurrentState());
        }

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
        maxHorizontalSpeed = speedParam.ParamValue * 0.1f + 1;
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        maxVerticalSpeed = maxHorizontalSpeed;
        baseAcceleration = acceleration;
        baseDeceleration = deceleration;
        baseMaxHorizontalSpeed = maxHorizontalSpeed;
        baseMaxVerticalSpeed = maxVerticalSpeed;
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

        acceleration = baseAcceleration * state.GetSpeedModifier() * buff.speedBuffFactor;
        deceleration = baseDeceleration * state.GetSpeedModifier() * buff.speedBuffFactor;
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier() * buff.speedBuffFactor;
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier() * buff.speedBuffFactor;
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
        buff = EmptyBuff();

        acceleration = baseAcceleration * state.GetSpeedModifier();
        maxHorizontalSpeed = baseMaxHorizontalSpeed * state.GetSpeedModifier();
        maxVerticalSpeed = baseMaxVerticalSpeed * state.GetSpeedModifier();
    }

    private BuffWallDefender EmptyBuff()
    {
        BuffWallDefender buffWallDefender = new BuffWallDefender();
        buffWallDefender.speedBuffFactor = 1f;
        buffWallDefender.attackBuffFactor = 1f;

        return buffWallDefender;
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameManager>().ProcessDefenceFailure();
    }
}
