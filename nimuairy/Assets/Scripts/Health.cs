using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 100f;
    [SerializeField] float currenttHealth;


    void Start()
    {
        currenttHealth = startHealth;
    }

    public void DealDamage(float damage)
    {
        currenttHealth -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (currenttHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
