using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSlanting : EnemyMovement
{

    [SerializeField] float timeToWaitToRotate = 0.75f;
    [SerializeField] float timeToChangeVerticalDirection = 5f;
    [SerializeField] float timeElapsedSinceLastDirectionChange = 0f;
    [SerializeField] int verticalDirection  = 1;
	

    public BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>())
        {
            arrivedAtWall = true;
        }

        if (collision.gameObject.GetComponent<WorldLimiter>())
        {
            verticalDirection *= -1;
            timeElapsedSinceLastDirectionChange = 0f;
        }
    }

    protected override void ProcessStandardMove()
    {
        HandleVerticalDirection();
        HandleMovement();
    }

    private void HandleVerticalDirection()
    {
        float timeFactor = 1f;
        if (enemyTimeManagerReacting.isReactingToFieldDefenderTimeFactor)
        {
            timeFactor = TimeManager.playerTimeFactor;
            animator.speed = TimeManager.playerTimeFactor;
        }

        timeElapsedSinceLastDirectionChange += Time.deltaTime * timeFactor;
        if (timeElapsedSinceLastDirectionChange > timeToChangeVerticalDirection)
        {
            verticalDirection *= -1;
            timeElapsedSinceLastDirectionChange = 0f;
        }
    }

    private void HandleMovement()
    {
        Vector2 direction = Vector2.left + Vector2.up * verticalDirection;
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    public override void SetStandardMoveAllowed(bool newValue)
    {
        base.SetStandardMoveAllowed(newValue);
        StartCoroutine(ProcessRotatingAfterAWhile());
    }

    public override void StartMoveInStandardWay()
    {
        if (!isStandardMoveAllowed)
        {
            isStandardMoveAllowed = true;
            StartCoroutine(ProcessRotatingAfterAWhile());
        }
    }

    IEnumerator ProcessRotatingAfterAWhile()
    {

        yield return new WaitForSeconds(timeToWaitToRotate);

        transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
    }

}
