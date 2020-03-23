using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderSlot : MonoBehaviour
{
    [SerializeField] WallDefender wallDefender;

    private void Start()
    {
        wallDefender.transform.position = transform.position;
    }
}
