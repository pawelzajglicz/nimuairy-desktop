using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDefenderMovement : MonoBehaviour
{
    [SerializeField] float speedHorizontal = 0;
    [SerializeField] float speedVertical = 0;
    [SerializeField] float maxHorizontalSpeed = 10;
    [SerializeField] float maxVerticalSpeed = 8;
    [SerializeField] float acceleration = 10;
    [SerializeField] float deceleration = 10;

    [SerializeField] float startMaxHorizontalSpeed;
    [SerializeField] float startMaxVerticalSpeed;
    [SerializeField] float startAcceleration;
    [SerializeField] float startDeceleration;

    StateManager stateManager;
    [SerializeField] State state;

    public Vector2 direction;


    public State State => state;
    public int FacingRightValue => facingRight;

    int facingRight = 1;

    private void Start()
    {
        startAcceleration = acceleration;
        startDeceleration = deceleration;
        startMaxHorizontalSpeed = maxHorizontalSpeed;
        startMaxVerticalSpeed = maxVerticalSpeed;

        stateManager = GetComponent<StateManager>();
        if (stateManager)
        {
            SetState(stateManager.getCurrentState());
        }
    }

    void Update()
    {
        HandleMoving();
    }

    private void HandleMoving()
    {
        CalculatingHorizontalSpeed();
        CalculatingVerticalSpeed();

        Vector2 vectorOffset = new Vector2(speedHorizontal * Time.deltaTime, speedVertical * Time.deltaTime);
        direction = vectorOffset.normalized;
        transform.position += (Vector3)vectorOffset;
        HandleFacingDirection();
    }

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

        acceleration = startAcceleration * state.GetSpeedModifier();
        deceleration = startDeceleration * state.GetSpeedModifier();
        maxHorizontalSpeed = startMaxHorizontalSpeed * state.GetSpeedModifier();
        maxVerticalSpeed = startMaxVerticalSpeed * state.GetSpeedModifier();
    }


    internal void ModifyMaxVerticalSpeedByFactor(float speedBuffFactor)
    {
        maxVerticalSpeed *= speedBuffFactor;
    }

    internal void ModifyMaxHorizontalSpeedByFactor(float speedBuffFactor)
    {
        maxHorizontalSpeed *= speedBuffFactor;
    }

    internal void ModifyAccelerationByFactor(float speedBuffFactor)
    {
        acceleration *= speedBuffFactor;
    }
}
