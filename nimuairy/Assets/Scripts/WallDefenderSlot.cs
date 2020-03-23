using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderSlot : MonoBehaviour
{
    [SerializeField] WallDefender wallDefender = null;

    private void Start()
    {
        if (wallDefender)
        {
            wallDefender.transform.position = transform.position;
        }
    }
}
