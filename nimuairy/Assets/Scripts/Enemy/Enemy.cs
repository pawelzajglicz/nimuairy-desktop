using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float biggerity = 1;
    [SerializeField] public int basicMagicCrystalReward = 2;
    [SerializeField] public int currentMagicCrystalReward;
    [SerializeField] float biggerityModificatorMinRange = 0f;
    [SerializeField] float biggerityModificatorMaxRange = 1.3f;

    [SerializeField] GameObject deathVfx;
    [SerializeField] float durationOfDeathExplosion;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathSoundVolume = 0.5f;

    GameManager gameManager;

    private void Awake()
    {
        biggerity += Random.Range(biggerityModificatorMinRange, biggerityModificatorMaxRange);
    }

    void Start()
    {
        currentMagicCrystalReward = basicMagicCrystalReward;
        transform.localScale *= biggerity;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnDestroy()
    {
        if (gameManager.isGameOver)
        {
            CleanDestroy();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosion = Instantiate(deathVfx, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfDeathExplosion);
        gameManager.HandleEnemyDeath();
        FindObjectOfType<ResourcesManager>().AddMagicCrystals(currentMagicCrystalReward);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void CleanDestroy()
    {

    }
}
