using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill
{

    [SerializeField] public float dashDistance = 3.5f;
    [SerializeField] public float dashDuration = 0.25f;
    public FieldDefenderMovement fieldDefenderMovement;

    private void Awake()
    {
        activationKey = KeyCode.A;
    }

    protected override void Start()
    {
        base.Start();
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    protected override void ActivateSkill()
    {
        StartCoroutine(Dashing());
    }

    public IEnumerator Dashing()
    {
        float elapsedTime = 0;
        Vector2 start = fieldDefenderMovement.transform.position;

        float xOffset = dashDistance * fieldDefenderMovement.FacingRightValue;
        Vector2 direction = fieldDefenderMovement.direction;// = new Vector2(start.x + xOffset, start.y);

        if (direction == Vector2.zero)
        {
            direction = new Vector2(fieldDefenderMovement.FacingRightValue, 0f);
        }

        Vector2 destination = start + (direction * dashDistance);

        while (elapsedTime < dashDuration)
        {
            fieldDefenderMovement.transform.position = Vector2.Lerp(start, destination, (elapsedTime / dashDuration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
