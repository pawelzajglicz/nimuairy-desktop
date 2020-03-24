using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialAction : WallDefenderAction
{

    [SerializeField] public WallDefenderAction actionToMultipleShot;

    [SerializeField] public int shotAmounts = 6;
    [SerializeField] public int currentShotAmounts;

    [SerializeField] public float timeInterval = 0.35f;
    [SerializeField] public float timeFromLastShot;


    private void Update()
    {
        timeFromLastShot += Time.deltaTime;
        ManageShot();
    }

    private void ManageShot()
    {
        if (timeFromLastShot > timeInterval)
        {
            WallDefenderAction action = Instantiate(actionToMultipleShot, transform.position, transform.rotation) as WallDefenderAction;
            action.SetTarget(targetPosition);
            timeFromLastShot = 0;
            ManageLimit();
        }
    }

    private void ManageLimit()
    {
        currentShotAmounts++;
        if (currentShotAmounts >= shotAmounts)
        {
            Destroy(gameObject);
        }
    }
}
