using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ParamUpgrader : MonoBehaviour
{
    [SerializeField] protected TextMeshPro currentValueTMP;
    [SerializeField] protected TextMeshPro nextValueTMP;
    [SerializeField] protected TextMeshPro costTMP;

    [SerializeField] public Param param;

    private string startCurrentValueText;
    private string startNextValueText;
    private string startCostText;

    [SerializeField] public int level = 1;
    [SerializeField] public int startCurrentValue = 5;
    [SerializeField] public int startCost = 2;

    [SerializeField] public int currentValue;
    [SerializeField] public int nextValue;
    [SerializeField] public int cost;

    [SerializeField] public float nextValueFactor = 0.3f;
    [SerializeField] public int addingValue = 2;

    [SerializeField] public float costFactor = 0.45f;
    [SerializeField] public int addingCost = 3;

    void Start()
    {
        startCurrentValueText = currentValueTMP.text;
        startNextValueText = nextValueTMP.text;
        startCostText = costTMP.text;

        nextValue = startCurrentValue + (int)(level * nextValueFactor + addingValue);

        UpdateNumbers();
        UpdateTexts();
    }

    void UpdateNumbers()
    {
        currentValue += (int)(level * nextValueFactor + addingValue);
        nextValue = currentValue + (int)((level + 1) * nextValueFactor + addingValue);
        cost += (int)(level * costFactor + addingCost);
    }

    void UpdateTexts()
    {
        currentValueTMP.text = startCurrentValueText + currentValue;
        nextValueTMP.text = startNextValueText + nextValue;
        costTMP.text = startCostText + cost;
    }
    

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManageNextLevel();
        }
    }

    private void ManageNextLevel()
    {
        level++;
        UpdateNumbers();
        UpdateTexts();
    }
}
