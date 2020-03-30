using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float biggerity = 1;
    [SerializeField] public int magicCrystalReward = 67;
    [SerializeField] float biggerityModificatorMinRange = 0f;
    [SerializeField] float biggerityModificatorMaxRange = 1.3f;

    GameManager gameManager;

    private void Awake()
    {
        biggerity += Random.Range(biggerityModificatorMinRange, biggerityModificatorMaxRange);
    }

    void Start()
    {
        transform.localScale *= biggerity;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnDestroy()
    {
        gameManager.HandleEnemyDeath();
        FindObjectOfType<ResourcesManager>().AddMagicCrystals(magicCrystalReward);
    }
}
