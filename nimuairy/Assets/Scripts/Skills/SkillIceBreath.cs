using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIceBreath : Skill
{
    [SerializeField] public Vector2 offset;
    public FieldDefenderMovement fieldDefenderMovement;


    [SerializeField] public float lifeTime = 1.5f;
    [SerializeField] public float speed = 2.5f;

    [SerializeField] public IceBallActionAtColliding wizardIceFastAction;

    private void Awake()
    {
        activationKey = KeyCode.D; ;
    }

    protected override void Start()
    {
        base.Start();
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    protected override void ActivateSkill()
    {
        IceBallActionAtColliding wizardIceFastActionInstance = Instantiate(wizardIceFastAction, fieldDefenderMovement.transform.position, transform.rotation) as IceBallActionAtColliding;
        wizardIceFastActionInstance.paramValueLifeTime = paramValue;

        Vector2 target = new Vector2(fieldDefenderMovement.transform.position.x + 100 * fieldDefenderMovement.FacingRightValue, fieldDefenderMovement.transform.position.y);
        wizardIceFastActionInstance.GetComponent<AttackWallDefenderSimpleStraight>().isDirectingToRight = fieldDefenderMovement.FacingRightValue == 1;
        wizardIceFastActionInstance.gameObject.GetComponent<WallDefenderAction>().SetTarget(target);

        wizardIceFastActionInstance.gameObject.GetComponent<WallDefenderAction>().speed = this.speed;

        Destroy(wizardIceFastActionInstance.gameObject, lifeTime);
        AudioSource.PlayClipAtPoint(skillSound, Camera.main.transform.position, skillSoundVolume);
    }
}
