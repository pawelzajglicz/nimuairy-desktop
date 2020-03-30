using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesNumberShower : MonoBehaviour
{

    [SerializeField] protected TextMeshPro text;

    int enemiesNumber;
    string startingText;
    
    private void Start()
    {
        FindText();
        CountEnemies();
    }

    private void FindText()
    {
        text = GetComponent<TextMeshPro>();
        startingText = text.text;
    }

    public void HandleEnemyDeath()
    {
        enemiesNumber--;
        UpdateText();
    }

    public void CountEnemies()
    {
        enemiesNumber = FindObjectsOfType<Enemy>().Length;
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = startingText + " " + enemiesNumber;
    }
}

