using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlackHole : Skill
{
    public FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] public BlackHole blackHole;

    private void Awake()
    {
        activationKey = KeyCode.F;
    }

    protected override void Start()
    {
        base.Start();
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }


    protected override void ActivateSkill()
    {
        BlackHole blackHoleinstance = Instantiate(blackHole, fieldDefenderMovement.transform.position, Quaternion.identity) as BlackHole;
        blackHoleinstance.pullingForceValue = paramValue * 0.0003f;
        AudioSource.PlayClipAtPoint(skillSound, Camera.main.transform.position, skillSoundVolume);
    }

   
}
