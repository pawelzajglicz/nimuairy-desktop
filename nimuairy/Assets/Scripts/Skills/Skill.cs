using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] public float cooldown = 4f;
    [SerializeField] public bool canBeActivated = true;

    public KeyCode activationKey;

    void Update()
    {
        if (Input.GetKeyDown(activationKey) && canBeActivated)
        {
            ActivateSkill();
            StartCoroutine(ManageCooldown());
        }
    }

    protected abstract void ActivateSkill();

    protected IEnumerator ManageCooldown()
    {
        canBeActivated = false;
        yield return new WaitForSeconds(cooldown);
        canBeActivated = true;
    }
}
