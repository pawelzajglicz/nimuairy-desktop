using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] public float cooldown = 4f;
    [SerializeField] public float cooldownRemaining;

    [SerializeField] public bool stillCooldown = false;

    public KeyCode activationKey;

    void Update()
    {
        ManageCooldownTime();
        ManageSkillActivability();
        ManageSkillUsing();
    }

    private void ManageCooldownTime()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= (Time.deltaTime * TimeManager.playerTimeFactor);
        }
    }

    private void ManageSkillActivability()
    {
        if (cooldownRemaining <= 0)
        {
            stillCooldown = false;
        }
    }

    private void ManageSkillUsing()
    {
        if (Input.GetKeyDown(activationKey) && !stillCooldown && GameManager.IsBattle)
        {
            ActivateSkill();
            stillCooldown = true;
            cooldownRemaining = cooldown;
        }
    }

    protected abstract void ActivateSkill();

}
