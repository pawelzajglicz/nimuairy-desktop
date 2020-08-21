using System;
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
    [SerializeField] SelectedWallDefenderSetter setter;

    internal void SetTargetDefaultPosition()
    {
        SetTargetPosition(new Vector3(0, 0, 0));
    }

    private void Start()
    {
        setter = FindObjectOfType<SelectedWallDefenderSetter>();
        /*if (wallDefender)
        {
            wallDefender.transform.position = transform.position;
            wallDefender.slot = this;
            isWallDefenderManualDefending = wallDefender.isManualTargeting;
        }*/
    }

    public void SetTargetPosition(Vector3 newPosition)
    {
        target.transform.position = newPosition;
    }

    public Vector2 GetTargetPosition()
    {
        return target.transform.position;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDefender(setter.wallDefender);
        }
    }

    public void SetDefender(WallDefender wallDefender)
    {
        if (this.wallDefender != null)
        {
            this.wallDefender.ReturnToPlaceholder(); ;
        }
        if (wallDefender.slot != null)
        {
            wallDefender.slot.wallDefender = null;
        }

        this.wallDefender = wallDefender;
        wallDefender.transform.parent = this.gameObject.transform;
        wallDefender.slot = this;
        wallDefender.transform.position = transform.position;
        isWallDefenderManualDefending = wallDefender.isManualTargeting;
        wallDefender.isActive = true;

        if (wallDefender.isManualTargeting)
        {
            SetTargetDefaultPosition();
        }
        else
        {
            SetTargetPosition(transform.position);
        }
    }
}
