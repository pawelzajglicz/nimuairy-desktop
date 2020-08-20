using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedFeatures : MonoBehaviour
{
    HashSet<LockedFeature> lockedFeatures;



    private void Start()
    {
        lockedFeatures = new HashSet<LockedFeature>();
        GetLockedFeatures();
    }

    private void GetLockedFeatures()
    {
        foreach (Transform child in transform)
        {
            LockedFeature lockedFeature = child.GetComponent<LockedFeature>();

            if (lockedFeature != null)
            {
                lockedFeatures.Add(lockedFeature);
            }
        }
    }

    public void TryUnlockFeatures()
    {
        Debug.Log("TryUnlocking");
        HashSet<LockedFeature> unlockedFeatures = new HashSet<LockedFeature>();

        foreach (LockedFeature lockedFeature in lockedFeatures)
        {
            bool wasUnlocked = lockedFeature.TryUnlock();
            Debug.Log(wasUnlocked);
            if (wasUnlocked)
            {
                unlockedFeatures.Add(lockedFeature);
            }
        }

        foreach (LockedFeature unlockedFeature in unlockedFeatures)
        {
            lockedFeatures.Remove(unlockedFeature);
        }

    }
}
