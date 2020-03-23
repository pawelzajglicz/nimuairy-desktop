using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderPriestess : TargetFinder
{

    [SerializeField] FieldDefenderMovement fieldDefender;
    [SerializeField] GameObject fieldDefenderGameObject;
    [SerializeField] GameObject wallGameObject;

    public bool isManualTargeting;

    private void Start()
    {
        fieldDefender = FindObjectOfType<FieldDefenderMovement>();
        fieldDefenderGameObject = fieldDefender.gameObject;
        wallGameObject = FindObjectOfType<Wall>().gameObject;
    }

    

    public override GameObject FindTarget()
    {
        bool isFieldDefenderStateInDefenceState = fieldDefender.GetComponent<StateManager>().IsInDefenceState();

        if (isFieldDefenderStateInDefenceState)
        {
            return fieldDefenderGameObject;
        }
        else
        {
            return wallGameObject;
        }
    }

   
}
