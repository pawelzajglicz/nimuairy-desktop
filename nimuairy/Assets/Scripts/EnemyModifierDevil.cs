using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifierDevil : EnemyModifier
{
    [SerializeField] public FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] public float activationDistance = 3f;
    [SerializeField] public float speed = 7f;

    private int facingLeftValue = 1;

    private void Awake()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    void Start()
    {
        isModifierActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckModifierActivationConditions();

        if (isModifierActive)
        {
            ProcessModifierAction();
        }
    }

    private void CheckModifierActivationConditions()
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

    private void ProcessModifierAction()
    {
        Vector2 direction = (transform.position - fieldDefenderMovement.transform.position).normalized;
        HandleFacingDirection(direction);
        transform.parent.transform.Translate(direction * speed * Time.deltaTime * facingLeftValue);
    }

    private void HandleFacingDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            transform.parent.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            facingLeftValue = 1;
        }
        else
        {
            transform.parent.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            facingLeftValue = -1;
        }
    }
}
