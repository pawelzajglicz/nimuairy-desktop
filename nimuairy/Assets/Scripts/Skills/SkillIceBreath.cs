using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIceBreath : Skill
{
    [SerializeField] public Vector2 offset;
    public FieldDefenderMovement fieldDefenderMovement;


    [SerializeField] public float lifeTime = 1.5f;
    [SerializeField] public float speed = 2.5f;

    [SerializeField] public WallDefenderAction wizardIceFastAction;

    private void Awake()
    {
        activationKey = KeyCode.D; ;
    }

    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    protected override void ActivateSkill()
    {
        WallDefenderAction wizardIceFastActionInstance = Instantiate(wizardIceFastAction, fieldDefenderMovement.transform.position, transform.rotation) as WallDefenderAction;

        Vector2 target = new Vector2(fieldDefenderMovement.transform.position.x + 100 * fieldDefenderMovement.FacingRightValue, fieldDefenderMovement.transform.position.y);
        wizardIceFastActionInstance.GetComponent<AttackWallDefenderSimpleStraight>().isDirectingToRight = fieldDefenderMovement.FacingRightValue == 1;
        wizardIceFastActionInstance.SetTarget(target);

        wizardIceFastActionInstance.speed = this.speed;

        Destroy(wizardIceFastActionInstance.gameObject, lifeTime);
    }
}
