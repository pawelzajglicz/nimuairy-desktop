﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWallDefenderExpandedToEnemy : WallDefenderAction
{

    [SerializeField] float lifeTimeLimit = 1.5f;
    Vector2 source;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTimeLimit);
        source = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGameObject != null)
        {
            ProcessAttacking();
        }
    }

    private void ProcessAttacking()
    {
        Vector2 target = targetGameObject.transform.position;
        Vector2 newLookAt = HolisticMath.LookAt2D(new Coords(transform.up), new Coords(this.transform.position), new Coords(target)).ToVector();
        if (newLookAt.magnitude > Mathf.Epsilon)
        {
            transform.up = newLookAt;
        }

        float newPositionX = (source.x + target.x) / 2;
        float newPositionY = (source.y + target.y) / 2;
        transform.position = new Vector2(newPositionX, newPositionY);

        float newScaleY = Mathf.Abs(target.x - source.x) + Mathf.Abs(target.y - source.y);
        transform.localScale = new Vector2(newScaleY, 5);
    }
}
