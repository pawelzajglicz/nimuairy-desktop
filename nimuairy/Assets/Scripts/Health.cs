using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 100f;
    [SerializeField] float maxHealth;
    [SerializeField] float currenttHealth;
    [SerializeField] float healFactor = 1f;
    [SerializeField] float damageFactor = 1f;
    [SerializeField] PercentageBar healthBar;


    void Start()
    {
        currenttHealth = startHealth;
        maxHealth = startHealth;
        TryGetHealthBar();
    }

    public void Reset()
    {
        currenttHealth = startHealth;
        healthBar.SetPercentage(100);
    }

    public void DealDamage(float damage)
    {
        currenttHealth -= damage * damageFactor;
        CheckDeath();
        HandleHealthBar();
    }

    public void Heal(float healPoints)
    {
        currenttHealth += healPoints * healFactor;
        HandleOverheal();
        HandleHealthBar();
    }

    private void HandleOverheal()
    {
        if (currenttHealth > maxHealth)
        {
            currenttHealth = maxHealth;
        }
    }

    private void HandleHealthBar()
    {
        if (healthBar)
        {
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currenttHealth * 100 / startHealth;
        healthBar.SetPercentage(healthPercentage);
    }

    private void CheckDeath()
    {
        if (currenttHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void TryGetHealthBar()
    {
        foreach (Transform child in transform)
        {
            PercentageBar childHealthBar = child.transform.gameObject.GetComponent<PercentageBar>();
            if (childHealthBar)
            {
                healthBar = childHealthBar;
                break;
            }
        }
    }
}
