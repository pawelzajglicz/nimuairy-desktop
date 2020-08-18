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
    [SerializeField] protected TextMeshPro maxLevelTMP;

    [SerializeField] public Param param;

    [SerializeField] AudioClip upgradeSound;
    [SerializeField] float upgradeSoundVolume = 0.5f;

    private string startCurrentValueText;
    private string startNextValueText;
    private string startCostText;
    private bool levelableUp = true;

    [SerializeField] public int level = 0;
    [SerializeField] public float startCurrentValue = 5;
    [SerializeField] public int startCost = 2;

    [SerializeField] public float currentValue;
    [SerializeField] public float nextValue;
    [SerializeField] public int cost;

    [SerializeField] public float nextValueFactor = 0.3f;
    [SerializeField] public float addingValue = 2;

    [SerializeField] public float costFactor = 0.45f;
    [SerializeField] public int addingCost = 3;

    [SerializeField] public bool isMaxLevel;
    [SerializeField] public int maxLevel = 20;

    void Start()
    {
        startCurrentValueText = currentValueTMP.text;
        startNextValueText = nextValueTMP.text;
        startCostText = costTMP.text;

        currentValue = startCurrentValue;
        nextValue = startCurrentValue + (int)(level * nextValueFactor + addingValue);
        UpdateNumbers();
        UpdateTexts();
        param.ParamValue = currentValue;
        FindObjectOfType<GameManager>().UpdateParamizables();

    }

    private void ShowMax()
    {
        nextValueTMP.gameObject.SetActive(false);
        costTMP.gameObject.SetActive(false);
        maxLevelTMP.gameObject.SetActive(true);
    }

    void UpdateNumbers()
    {
        currentValue += additionalValuePerLevel(level);
        nextValue = currentValue + additionalValuePerLevel(level + 1);
        cost += (int)(level * costFactor + addingCost);
    }

    int additionalValuePerLevel(int levelForAdditional)
    {
        return (int)(levelForAdditional * nextValueFactor + addingValue);
    }

    void UpdateTexts()
    {
        currentValueTMP.text = startCurrentValueText + currentValue;
        nextValueTMP.text = startNextValueText + nextValue;
        costTMP.text = startCostText + cost;
    }


    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && levelableUp)
        {
            if (IsEnoughMagicCrystals())
            {
                ManageNextLevel();
            }
        }
    }

    private bool IsEnoughMagicCrystals()
    {
        return cost <= FindObjectOfType<ResourcesManager>().MagicCrystalsAmount;
    }

    private void ManageNextLevel()
    {
        level++;
        FindObjectOfType<ResourcesManager>().RemoveMagicCrystals(cost);
        UpdateNumbers();
        UpdateTexts();


        if (isMaxLevel && level >= maxLevel)
        {
            ShowMax();
            levelableUp = false;
        }

        param.ParamValue = currentValue;

        AudioSource.PlayClipAtPoint(upgradeSound, Camera.main.transform.position, upgradeSoundVolume);
    }
}
