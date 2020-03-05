using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifierDevil : EnemyModifier
{
    [SerializeField] public FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] public float activationDistance = 3f;
    [SerializeField] public float speed = 7f;


    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }


    protected override void CheckModifierActivationConditions()
    {
        float distance = Vector2.Distance(transform.position, fieldDefenderMovement.transform.position);

        if (distance <= activationDistance && IsFieldDefenderInDefenceState())
        {
            isModifierActive = true;
        }
        else
        {
            isModifierActive = false;
        }
    }

    private bool IsFieldDefenderInDefenceState()
    {
        return fieldDefenderMovement.GetComponent<StateManager>().IsInDefenceState();
    }

    protected override void ProcessModifierAction()
    {
        Vector2 direction = (transform.position - fieldDefenderMovement.transform.position).normalized;
        HandleFacingDirection(direction);
        transform.parent.transform.Translate(direction * speed * Time.deltaTime * facingLeftValue);
    }
}
