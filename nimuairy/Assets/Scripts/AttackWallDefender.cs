using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefender : MonoBehaviour
{
    [SerializeField] public float attackPower = 10f;
    [SerializeField] public float speed = 1f;
    [SerializeField] public Vector2 target;

    public void SetTarget()
    {

    }

    public void SetTarget(Enemy enemy)
    {
        
    }

    public void SetTarget(Vector2 newTarget)
    {
        target = newTarget;
        if (newTarget != Vector2.zero)
        {
            transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(newTarget)).ToVector();
        }
    }

}
