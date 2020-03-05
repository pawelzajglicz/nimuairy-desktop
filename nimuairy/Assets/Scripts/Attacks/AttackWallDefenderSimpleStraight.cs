using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraight : AttackWallDefender
{
    float stoppingDistance = 0.1f;

    void Update()
    {
        float distance = Mathf.Abs((targetPostion - (Vector2)transform.position).magnitude);
        CheckTargetAchieving(distance);
        MoveTotarget();
    }

    private void CheckTargetAchieving(float distance)
    {
        if (distance < stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTotarget()
    {
        Vector2 direction = (targetPostion - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
