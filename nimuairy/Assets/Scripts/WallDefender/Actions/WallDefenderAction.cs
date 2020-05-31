﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderAction : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] public GameObject targetGameObject;

    [SerializeField] public float factorFromWallDefender;
    [SerializeField] public float paramValue;
    
    public void SetTarget()
    {

    }

    public void SetTarget(Enemy enemy)
    {

    }

    public void setFactorFromWallDefender(float f)
    {
        factorFromWallDefender = f;
    }

    public void SetTarget(Vector2 newTarget)
    {
        targetPosition = newTarget;
        if (newTarget != Vector2.zero && ((Vector2)this.transform.position != targetPosition))
        {
            Debug.Log("aa " + HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(newTarget)).ToVector());
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
                Debug.Log("aab " + HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(targetPosition)).ToVector());
                transform.up = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(targetPosition)).ToVector();
            }
        }
    }
}

