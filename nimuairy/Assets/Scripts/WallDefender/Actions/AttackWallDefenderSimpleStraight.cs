using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraight : WallDefenderAction
{
    float stoppingDistance = 0.05f;
    public bool isDirectingToRight = true;

    void Update()
    {
        float distance = Mathf.Abs((targetPosition - (Vector2)transform.position).magnitude);
        CheckTargetAchieving(distance);
        MoveTotarget();
    }

    protected void CheckTargetAchieving(float distance)
    {
        if (distance < stoppingDistance || GoTooFar())
        {
            TargetAchievedAction();
        }
    }

    private bool GoTooFar()
    {
        if (isDirectingToRight)
        {
            return transform.position.x >= targetPosition.x;
        }
        else
        {
            return transform.position.x <= targetPosition.x;
        }
    }

    protected virtual void TargetAchievedAction()
    {
        Destroy(gameObject);
    }

    private void MoveTotarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
