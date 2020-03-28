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

    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }


    protected override void ActivateSkill()
    {
        Instantiate(blackHole, fieldDefenderMovement.transform.position, Quaternion.identity);
    }

   
}
