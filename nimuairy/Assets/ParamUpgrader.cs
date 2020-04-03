using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] public float nextValueFactor = 0.05f;
    [SerializeField] public int addingValue = 2;

    [SerializeField] public float costFactor = 0.05f;
    [SerializeField] public int addingCost = 2;

    void Start()
    {
        startCurrentValueText = currentValueTMP.text;
        startNextValueText = nextValueTMP.text;
        startCostText = costTMP.text;

        UpdateNumbers();
        UpdateTexts();
    }

    void UpdateNumbers()
    {
        currentValue = (int)(level * nextValueFactor + addingValue);
        nextValue = (int)((level + 1) * nextValueFactor + addingValue);
        cost = (int)(level * costFactor + addingCost);
    }

    void UpdateTexts()
    {
        currentValueTMP.text = startCurrentValueText + currentValue;
        nextValueTMP.text = startNextValueText + nextValue;
        costTMP.text = startCostText + cost;
    }
}
