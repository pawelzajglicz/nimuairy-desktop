﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForWallDefender : MonoBehaviour
{
    [SerializeField] WallDefender wallDefender;
    [SerializeField] WallDefenderSlot slot;

    void Start()
    {
        wallDefender = slot.wallDefender;
        if (wallDefender && wallDefender.isManualTargeting)
        {
            transform.position = Vector2.zero;
        }
    }
}
