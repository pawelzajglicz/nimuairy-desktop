using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefender : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] public Vector2 targetPostion;
    [SerializeField] public GameObject targetGameObject;

    public void SetTarget()
    {

    }

    public void SetTarget(Enemy enemy)
    {

    }

    public void SetTarget(Vector2 newTarget)
    {
        targetPostion = newTarget;
        if (newTarget != Vector2.zero)
        {
            transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(newTarget)).ToVector();
        }
    }

    public void SetTarget(GameObject gameObject)
    {
        targetGameObject = gameObject;
        if (gameObject != null)
        {
            targetPostion = gameObject.transform.position;

            if (targetPostion != Vector2.zero)
            {
                transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(targetPostion)).ToVector();
            }
        }
    }
}

