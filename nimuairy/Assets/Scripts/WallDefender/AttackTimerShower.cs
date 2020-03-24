using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimerShower : MonoBehaviour
{
    [SerializeField] public float fastAttackInterval;
    [SerializeField] public float timeSinceFastAttack;

    [SerializeField] public float slowAttackInterval;
    [SerializeField] public float timeSinceSlowAttack;


    [SerializeField] PercentageBar fastAttackBar;
    [SerializeField] PercentageBar slowAttackBar;

    [SerializeField] WallDefender wallDefender;

    private void Start()
    {
        wallDefender = GetComponent<WallDefender>();

        fastAttackInterval = wallDefender.fastActionInterval;
        slowAttackInterval = wallDefender.slowActionInterval;
    }

    private void Update()
    {
        timeSinceFastAttack = wallDefender.timeToFastAction;
        timeSinceSlowAttack = wallDefender.timeToSlowAction;

        fastAttackBar.SetPercentage(timeSinceFastAttack * 100 / fastAttackInterval);
        slowAttackBar.SetPercentage(timeSinceSlowAttack * 100 / slowAttackInterval);
    }
}
