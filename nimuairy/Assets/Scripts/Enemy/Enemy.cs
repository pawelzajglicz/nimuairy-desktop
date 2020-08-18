using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float biggerity = 1;
    [SerializeField] public int magicCrystalReward = 67;
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
        transform.localScale *= biggerity;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(deathVfx, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfDeathExplosion);
        gameManager.HandleEnemyDeath();
        FindObjectOfType<ResourcesManager>().AddMagicCrystals(magicCrystalReward);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
