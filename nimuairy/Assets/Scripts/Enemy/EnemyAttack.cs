using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public float attackPower = 35f;
    [SerializeField] public float lifeTime = 0.5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public float GetAttackPower()
    {
        return attackPower;
    }

    public float GetLifeTime()
    {
        return lifeTime;
    }
}
