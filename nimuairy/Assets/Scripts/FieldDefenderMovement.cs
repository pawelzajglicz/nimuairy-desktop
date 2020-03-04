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

        transform.position = new Vector2(transform.position.x + speedHorizontal * Time.deltaTime, transform.position.y + speedVertical * Time.deltaTime);
        HandleFacingDirection();
    }

    private void CalculatingHorizontalSpeed()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (speedHorizontal > -maxHorizontalSpeed))
            speedHorizontal = speedHorizontal - acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (speedHorizontal < maxHorizontalSpeed))
            speedHorizontal = speedHorizontal + acceleration * Time.deltaTime;
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
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (speedVertical > -maxVerticalSpeed))
            speedVertical = speedVertical - acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (speedVertical < maxVerticalSpeed))
            speedVertical = speedVertical + acceleration * Time.deltaTime;
        else
        {
            if (speedVertical > deceleration * Time.deltaTime)
                speedVertical = speedVertical - deceleration * Time.deltaTime;
            else if (speedVertical < -deceleration * Time.deltaTime)
                speedVertical = speedVertical + deceleration * Time.deltaTime;
            else
                speedVertical = 0;
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

    public int GetFacingRightValue()
    {
        return facingRight;
    }

    public void SetState(State state)
    {
        this.state = state;

        acceleration = startAcceleration * state.GetSpeedModifier();
        deceleration = startDeceleration * state.GetSpeedModifier();
        maxHorizontalSpeed = startMaxHorizontalSpeed * state.GetSpeedModifier();
        maxVerticalSpeed = startMaxVerticalSpeed * state.GetSpeedModifier();
    }

    public State getState()
    {
        return state;
    }
}
