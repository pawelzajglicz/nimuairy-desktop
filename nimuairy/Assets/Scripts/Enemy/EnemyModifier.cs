using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifier : MonoBehaviour
{

    [SerializeField] public bool isModifierActive;
    private int facingLeftValue = 1;

    public bool IsModifierActive()
    {
        return isModifierActive;
    }

    void Awake()
    {
        isModifierActive = false;
    }

    void Update()
    {
        CheckModifierActivationConditions();

        if (isModifierActive)
        {
            ProcessModifierAction();
        }
    }

    protected virtual void CheckModifierActivationConditions() { }
    protected virtual void ProcessModifierAction() { }

    protected void HandleFacingDirection(Vector2 direction)
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
