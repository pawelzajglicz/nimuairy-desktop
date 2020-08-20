using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedFeature : MonoBehaviour
{
    [SerializeField] GameObject[] lockedFeatures;
    [SerializeField] int unlockingLevelNumber;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    internal bool TryUnlock()
    {
        if (gameManager.level >= unlockingLevelNumber)
        {
            Unlock();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Unlock()
    {
        for (int i = 0; i < lockedFeatures.Length; i++)
        {
            lockedFeatures[i].gameObject.SetActive(true);
        }
    }
}
