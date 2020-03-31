using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SuccessDisplayer success;
    [SerializeField] FailureDisplayer failure;

    public bool IsBattle;
    static int enemiesNumber;  


    public void StartBattle()
    {
        IsBattle = true;
        WallDefenderSlot[] slots = FindObjectsOfType<WallDefenderSlot>();

        foreach (WallDefenderSlot slot in slots)
        {
            slot.SetTargetDefaultPosition();
        }
    }

    public void StopBattle()
    {
        IsBattle = false;
    }


    public void HandleEnemyDeath()
    {
        enemiesNumber--;
        UpdateDisplayers();

        if (enemiesNumber <= 0)
        {
            ProcessDefenceSuccess();
        }
    }

    private void ProcessDefenceSuccess()
    {
        success.gameObject.SetActive(true);
    }

    public void CountEnemies()
    {
        enemiesNumber = FindObjectsOfType<Enemy>().Length;
        UpdateDisplayers();
    }

    private void UpdateDisplayers()
    {
        EnemiesNumberShower[] showers = FindObjectsOfType<EnemiesNumberShower>();
        foreach (EnemiesNumberShower shower in showers)
        {
            shower.SetEnemiesNumber(enemiesNumber);
        }
    }

}
