using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementStraight : EnemyMovement
{

    [SerializeField] float timeToWaitToRotate = 0.75f;
    [SerializeField] bool arrivedAtWall;

    public BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        isStandardMoveAllowed = true;
    }
    
    void Update()
    {
        if (isStandardMoveAllowed && !arrivedAtWall)
        {
            ProcessStandardMove();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>())
        {
            arrivedAtWall = true;
        }
    }

    private void ProcessStandardMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public override void SetStandardMoveAllowed(bool newValue)
    {
        base.SetStandardMoveAllowed(newValue);
        StartCoroutine(ProcessRotatingAfterAWhile());
    }

    public override void StartMove()
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
