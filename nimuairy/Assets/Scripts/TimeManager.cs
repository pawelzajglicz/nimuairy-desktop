using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField] float playerTimeFactor;
    [SerializeField] FieldDefenderMovement fieldDefender;

    
    void Start()
    {
        fieldDefender = FindObjectOfType<FieldDefenderMovement>();
    }
    
    void Update()
    {
        playerTimeFactor = fieldDefender.GetTimeFactor();
    }

    public void SetPlayerTimeFactor(float newFactor)
    {
        playerTimeFactor = newFactor;
    }

    public float GetPlayerTimeFactor()
    {
        return playerTimeFactor;
    }
}
