using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDefenderMovement : MonoBehaviour
{
    [SerializeField] float SpeedHorizontal = 0;
    [SerializeField] float SpeedVertical = 0;
    [SerializeField] float MaxHorizontalSpeed = 10;
    [SerializeField] float MaxVerticalSpeed = 8;
    [SerializeField] float Acceleration = 10;
    [SerializeField] float Deceleration = 10;

    int facingRight = 1;


    void Update()
    {
        HandleMoving();
    }

    private void HandleMoving()
    {
        CalculatingHorizontalSpeed();
        CalculatingVerticalSpeed();

        transform.position = new Vector2(transform.position.x + SpeedHorizontal * Time.deltaTime, transform.position.y + SpeedVertical * Time.deltaTime);
        HandleFacingDirection();
    }

    private void CalculatingHorizontalSpeed()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (SpeedHorizontal > -MaxHorizontalSpeed))
            SpeedHorizontal = SpeedHorizontal - Acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (SpeedHorizontal < MaxHorizontalSpeed))
            SpeedHorizontal = SpeedHorizontal + Acceleration * Time.deltaTime;
        else
        {
            if (SpeedHorizontal > Deceleration * Time.deltaTime)
                SpeedHorizontal = SpeedHorizontal - Deceleration * Time.deltaTime;
            else if (SpeedHorizontal < -Deceleration * Time.deltaTime)
                SpeedHorizontal = SpeedHorizontal + Deceleration * Time.deltaTime;
            else
                SpeedHorizontal = 0;
        }
    }

    private void CalculatingVerticalSpeed()
    {
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (SpeedVertical > -MaxVerticalSpeed))
            SpeedVertical = SpeedVertical - Acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (SpeedVertical < MaxVerticalSpeed))
            SpeedVertical = SpeedVertical + Acceleration * Time.deltaTime;
        else
        {
            if (SpeedVertical > Deceleration * Time.deltaTime)
                SpeedVertical = SpeedVertical - Deceleration * Time.deltaTime;
            else if (SpeedVertical < -Deceleration * Time.deltaTime)
                SpeedVertical = SpeedVertical + Deceleration * Time.deltaTime;
            else
                SpeedVertical = 0;
        }
    }

    private void HandleFacingDirection()
    {

        if (SpeedHorizontal < 0 && Mathf.Abs(SpeedHorizontal) > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            facingRight = -1;
        }
        else if (SpeedHorizontal > 0 && Mathf.Abs(SpeedHorizontal) > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            facingRight = 1;
        }
    }

    public int GetFacingRightValue()
    {
        return facingRight;
    }
}
