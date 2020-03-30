using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsBattle;
    static int enemiesNumber;  

    public void HandleEnemyDeath()
    {
        enemiesNumber--;
        UpdateDisplayers();
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
