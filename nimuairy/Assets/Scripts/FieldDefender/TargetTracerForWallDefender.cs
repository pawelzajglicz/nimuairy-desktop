using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracerForWallDefender : MonoBehaviour
{
    
    [SerializeField] WallDefenderSlot[] wallDefenderSlots = new WallDefenderSlot[4];
    [SerializeField] KeyCode[] keyCodes = new KeyCode[4];


    private void Start()
    {
        keyCodes = new KeyCode[4] { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };
    }

    void Update()
    {
        HandleKeys();
    }

    private void HandleKeys()
    {
        for (int i = 0; i < wallDefenderSlots.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]) && wallDefenderSlots[i].wallDefender && wallDefenderSlots[i].wallDefender.isManualTargeting)
            {
                wallDefenderSlots[i].SetTargetPosition(transform.position);
            }
        }
    }
    
}
