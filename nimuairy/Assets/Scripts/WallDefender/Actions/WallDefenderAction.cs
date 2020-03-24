using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderAction : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] public GameObject targetGameObject;

    public void SetTarget()
    {

    }

    public void SetTarget(Enemy enemy)
    {

    }

    public void SetTarget(Vector2 newTarget)
    {
        targetPosition = newTarget;
        if (newTarget != Vector2.zero && ((Vector2)this.transform.position != targetPosition))
        {
            transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(newTarget)).ToVector();
        }
    }

    public void SetTarget(GameObject gameObject)
    {
        targetGameObject = gameObject;
        if (gameObject != null)
        {
            targetPosition = gameObject.transform.position;

            if (targetPosition != Vector2.zero && ((Vector2)this.transform.position != targetPosition))
            {
                transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(targetPosition)).ToVector();
            }
        }
    }
}

