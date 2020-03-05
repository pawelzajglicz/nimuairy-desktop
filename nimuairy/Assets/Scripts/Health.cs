using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 100f;
    [SerializeField] float currenttHealth;
    [SerializeField] HealthBar healthBar;


    void Start()
    {
        currenttHealth = startHealth;
        TryGetHealthBar();
    }

    public void DealDamage(float damage)
    {
        currenttHealth -= damage;
        CheckDeath();
        HandleHealthBar();
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
            HealthBar childHealthBar = child.transform.gameObject.GetComponent<HealthBar>();
            if (childHealthBar)
            {
                healthBar = childHealthBar;
                break;
            }
        }
    }
}
