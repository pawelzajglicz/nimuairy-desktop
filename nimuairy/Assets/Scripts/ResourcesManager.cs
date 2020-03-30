using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] public int MagicCrystalsAmount;

    public void AddMagicCrystals(int amountToAdd)
    {
        MagicCrystalsAmount += amountToAdd;

        MagicCrystalAmountShower[] showers = FindObjectsOfType<MagicCrystalAmountShower>();
        foreach (MagicCrystalAmountShower shower in showers)
        {
            shower.UpdateCrystalAmount();
        }
    }
}
