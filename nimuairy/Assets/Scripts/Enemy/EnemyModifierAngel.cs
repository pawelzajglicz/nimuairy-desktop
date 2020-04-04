using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifierAngel : EnemyModifier
{
    [SerializeField] public FieldDefenderMovement fieldDefenderMovement;
    [SerializeField] public float angelModifierSpeed = 2.5f;

    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
        gameManager = FindObjectOfType<GameManager>();
        enemyMovement = transform.parent.gameObject.GetComponent<EnemyMovement>();
    }



    protected override void CheckModifierActivationConditions()
    {
        isModifierActive = IsFieldDefenderInAttackingState() && gameManager.IsBattle;
    }

    private bool IsFieldDefenderInAttackingState()
    {
        return fieldDefenderMovement.GetComponent<StateManager>().IsInAttackingState();
    }

    protected override void ProcessModifierAction()
    {
        MoveToFieldDefender();
    }

    private void MoveToFieldDefender()
    {
        Vector2 direction = (fieldDefenderMovement.transform.position - transform.parent.transform.position).normalized;
        HandleFacingDirection(direction);
        transform.parent.gameObject.transform.position = (Vector2)transform.parent.gameObject.transform.position + (direction * enemyMovement.currentSpeed * angelModifierSpeed * Time.deltaTime);
    }
}
