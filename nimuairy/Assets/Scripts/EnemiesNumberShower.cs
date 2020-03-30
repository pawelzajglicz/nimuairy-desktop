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
    }

    private void FindText()
    {
        text = GetComponent<TextMeshPro>();
        startingText = text.text;
    }

    public void UpdateText()
    {
        text.text = startingText + " " + enemiesNumber;
    }

    public void SetEnemiesNumber(int number)
    {
        enemiesNumber = number;
        UpdateText();
    }
}

