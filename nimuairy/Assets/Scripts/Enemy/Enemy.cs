using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float biggerity = 1;
    [SerializeField] float biggerityModificatorMinRange = 0f;
    [SerializeField] float biggerityModificatorMaxRange = 1.3f;

    private void Awake()
    {
        biggerity += Random.Range(biggerityModificatorMinRange, biggerityModificatorMaxRange);
    }

    void Start()
    {
        transform.localScale *= biggerity;
    }

    private void OnDestroy()
    {
        FindObjectOfType<EnemiesNumberShower>().HandleEnemyDeath();
    }
}
