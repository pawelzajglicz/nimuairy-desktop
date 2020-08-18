using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : Paramizable
{
    [SerializeField] public float cooldown = 4f;
    [SerializeField] public float cooldownRemaining;

    [SerializeField] public bool stillCooldown = false;

    [SerializeField] public float paramValue;
    [SerializeField] Param param;

    [SerializeField] protected AudioClip skillSound;
    [SerializeField] protected float skillSoundVolume = 0.5f;

    public KeyCode activationKey;


    GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

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
        if (Input.GetKeyDown(activationKey) && !stillCooldown && gameManager.IsBattle)
        {
            ActivateSkill();
            stillCooldown = true;
            cooldownRemaining = cooldown;
        }
    }

    public override void UpdateParams()
    {
        if (param != null)
        {
            paramValue = param.paramValue;
        }
    }

    protected abstract void ActivateSkill();

}
