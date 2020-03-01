using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderSimpleStraight : AttackWallDefender
{
    
    void Update()
    {
        float distance = Mathf.Abs((target - (Vector2)transform.position).magnitude);
        if (distance < 0.05f)
        {
            Destroy(gameObject);
        }

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
