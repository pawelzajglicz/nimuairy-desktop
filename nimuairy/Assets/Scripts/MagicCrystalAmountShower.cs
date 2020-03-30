using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicCrystalAmountShower : MonoBehaviour
{

    [SerializeField] protected TextMeshPro text;

    int magicCrystalNumber;
    string startingText;
    
    private void Start()
    {
        FindText();
        UpdateCrystalAmount();
    }

    private void FindText()
    {
        text = GetComponent<TextMeshPro>();
        startingText = text.text;
    }

    public void UpdateCrystalAmount()
    {
        magicCrystalNumber = FindObjectOfType<ResourcesManager>().MagicCrystalsAmount;
        UpdateText();
    }

    public void UpdateText()
    {
        text.text =  "" + magicCrystalNumber;
    }
}

