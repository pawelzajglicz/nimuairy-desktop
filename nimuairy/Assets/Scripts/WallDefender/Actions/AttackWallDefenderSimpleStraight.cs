using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraight : WallDefenderAction
{
    float stoppingDistance = 0.1f;

    void Update()
    {
        float distance = Mathf.Abs((targetPosition - (Vector2)transform.position).magnitude);
        CheckTargetAchieving(distance);
        MoveTotarget();
    }

    protected void CheckTargetAchieving(float distance)
    {
        if (distance < stoppingDistance || transform.position.x >= targetPosition.x)
        {
            TargetAchievedAction();
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
