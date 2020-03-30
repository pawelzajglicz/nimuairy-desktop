using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDragonBreath : Skill
{
    [SerializeField] public Vector2 offset;
    public FieldDefenderMovement fieldDefenderMovement;
    public DragonBreath dragonBreath;

    private void Awake()
    {
        activationKey = KeyCode.S;;
    }

    protected override void Start()
    {
        base.Start();
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    protected override void ActivateSkill()
    {
        Vector2 dragonBreathPosition = (Vector2)fieldDefenderMovement.transform.position + offset * fieldDefenderMovement.FacingRightValue;
        DragonBreath dragonBreathInstance = Instantiate(dragonBreath, dragonBreathPosition, Quaternion.identity) as DragonBreath;
        dragonBreathInstance.transform.parent = fieldDefenderMovement.transform;
    }

}
