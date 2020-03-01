using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraight : AttackWallDefender
{
    
    void Update()
    {
        float distance = Mathf.Abs((target - (Vector2)transform.position).magnitude);
        CheckTargetAchieving(distance);
        MoveTotarget();
    }

    private void CheckTargetAchieving(float distance)
    {
        if (distance < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTotarget()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
