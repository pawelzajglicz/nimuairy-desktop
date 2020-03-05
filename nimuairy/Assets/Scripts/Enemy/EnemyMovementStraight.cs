using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementStraight : EnemyMovement
{

    [SerializeField] float timeToWaitToRotate = 0.75f;

    void Start()
    {
        isStandardMoveAllowed = true;
    }
    
    void Update()
    {
        if (isStandardMoveAllowed)
        {
            ProcessStandardMove();
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
