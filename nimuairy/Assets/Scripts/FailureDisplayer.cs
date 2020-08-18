using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureDisplayer : MonoBehaviour
{
    [SerializeField] AudioClip failureSound;
    [SerializeField] float failureSoundVolume = 0.5f;

    internal void ManageFailure()
    {
        AudioSource.PlayClipAtPoint(failureSound, Camera.main.transform.position, failureSoundVolume);
    }
}
