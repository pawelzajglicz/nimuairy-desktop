using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float SpeedHorizontal = 0;
    [SerializeField] float SpeedVertical = 0;
    [SerializeField] float MaxSpeed = 10;
    [SerializeField] float Acceleration = 10;
    [SerializeField] float Deceleration = 10;


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
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (SpeedHorizontal > -MaxSpeed))
            SpeedHorizontal = SpeedHorizontal - Acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (SpeedHorizontal < MaxSpeed))
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
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (SpeedVertical > -MaxSpeed))
            SpeedVertical = SpeedVertical - Acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (SpeedVertical < MaxSpeed))
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
        }
        else if (SpeedHorizontal > 0 && Mathf.Abs(SpeedHorizontal) > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }
}
