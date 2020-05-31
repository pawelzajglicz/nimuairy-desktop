using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifiersManager : MonoBehaviour
{
    [SerializeField] List<EnemyModifier> enemyModifiers;
    [SerializeField] EnemyMovement enemyMovement;

    [SerializeField] bool isAnyModifierActive = false;
    private void Start()
    {
        enemyModifiers = new List<EnemyModifier>();
 
        FillEnemyModifiersCollection();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FillEnemyModifiersCollection()
    {
        EnemyModifier childEnemyModifier;
        foreach (Transform child in transform)
        {
            childEnemyModifier = child.transform.gameObject.GetComponent<EnemyModifier>();
            if (childEnemyModifier)
            {
                enemyModifiers.Add(childEnemyModifier);
            }
        }
    }

    private void Update()
    {
        ManageStandardMove();
    }

    private void ManageStandardMove()
    {
        isAnyModifierActive = false;
        foreach (EnemyModifier modifier in enemyModifiers)
        {
            if (modifier.IsModifierActive())
            {
                isAnyModifierActive = true;
                break;
            }
        }

        if (isAnyModifierActive)
        {
            enemyMovement.DontMoveInStandardWay();
        }
        else
        {
            enemyMovement.StartMoveInStandardWay();
        }
    }
}
