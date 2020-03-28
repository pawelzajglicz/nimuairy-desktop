using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderSlot : MonoBehaviour
{
    [SerializeField] int slotNumber;
    [SerializeField] bool isWallDefenderManualDefending;
    [SerializeField] public WallDefender wallDefender;
    [SerializeField] TargetForWallDefender targetPrefab;
    [SerializeField] TargetForWallDefender target;


    private void Start()
    {
        if (wallDefender)
        {
            wallDefender.transform.position = transform.position;
            wallDefender.slot = this;
            isWallDefenderManualDefending = wallDefender.isManualTargeting;
        }
    }

    public void SetTargetPosition(Vector3 newPosition)
    {
        target.transform.position = newPosition;
    }

    public Vector2 GetTargetPosition()
    {
        return target.transform.position;
    }
}
