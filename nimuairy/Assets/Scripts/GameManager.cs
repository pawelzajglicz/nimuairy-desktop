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

    public void ProcessDefenceSuccess()
    {
        IsBattle = false;
        success.gameObject.SetActive(true);
        TimeManager.playerTimeFactor = 0;

        ResetTimers();
        ResetHealth();
    }

    private void ResetHealth()
    {
        foreach (Health health in FindObjectsOfType<Health>())
        {
            health.Reset();
        }
    }

    public void ProcessDefenceFailure()
    {
        failure.gameObject.SetActive(true);
    }

    private void ResetTimers()
    {
        ResetWallDefendersTimers();
        ResetSkillTimers();
    }

    private void ResetWallDefendersTimers()
    {
        foreach (WallDefender defender in FindObjectsOfType<WallDefender>())
        {
            defender.timeToFastAction = defender.fastActionInterval;
            defender.timeToSlowAction = defender.slowActionInterval;
        }
    }

    private void ResetSkillTimers()
    {
        foreach (Skill skill in FindObjectsOfType<Skill>())
        {
            skill.cooldownRemaining = 0;
        }
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
