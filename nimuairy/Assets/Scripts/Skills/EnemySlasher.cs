using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlasher : MonoBehaviour
{

    [SerializeField] SlashTrace slashTrace;

    [SerializeField] int maxNumberOfTraces = 5;
    [SerializeField] int numberOfTraces;
    [SerializeField] float tracesShowingInterval = 0.75f;

    [SerializeField] float xLeftLimitInstantiate = -10f;
    [SerializeField] float xRightLimitInstantiate = 14f;
    [SerializeField] float yUpperLimitInstantiate = 3f;
    [SerializeField] float yBottomLimitInstantiate = -4f;

    public float paramAttackPowerValue;
    
    void Start()
    {
        InstantiateSlashTraces();
    }

    private void InstantiateSlashTraces()
    {
        StartCoroutine(InstantiateSlashTrace());
    }

    private IEnumerator InstantiateSlashTrace()
    {
        numberOfTraces += 1;

        float xPosition = UnityEngine.Random.Range(xLeftLimitInstantiate, xRightLimitInstantiate);
        float yPosition = UnityEngine.Random.Range(yBottomLimitInstantiate, yUpperLimitInstantiate);
        float rotation = UnityEngine.Random.Range(0, 90);

        SlashTrace slashTraceInstance = Instantiate(slashTrace, new Vector2(xPosition, yPosition), Quaternion.Euler(0, 0, rotation)) as SlashTrace;
        slashTraceInstance.attackPower = paramAttackPowerValue;

        yield return new WaitForSeconds(tracesShowingInterval);
        HandleNextActivity();
    }

    private void HandleNextActivity()
    {
        if (numberOfTraces < maxNumberOfTraces)
        {
            StartCoroutine(InstantiateSlashTrace());
        }
        else
        {
            StartCoroutine(SelfDestroy());
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(slashTrace.lifeTime);
        Destroy(gameObject);
    }
}
