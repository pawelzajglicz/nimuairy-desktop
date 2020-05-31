using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] float XLeftLimitInstantiate = -10f;
    [SerializeField] float XRightLimitInstantiate = 14f;
    [SerializeField] float YUpperLimitInstantiate = 3f;
    [SerializeField] float YBottomLimitInstantiate = -4f;

    [SerializeField] float xOffsetForNoReactingForTimeFactorEnemies = 26f;

    [SerializeField] public int NumberOfEnemiesReactingToTimeFactor = 18;

    [SerializeField] public int NumberOfEnemiesNoReactingToTimeFactor = 26;
    int allEnemiesAmount;

    [SerializeField] int numberOfEnemiesWalkingStraight = 26;
   /* [SerializeField]*/ int numberOfEnemiesWalkingSlant;

    [SerializeField] public int NumberAngelModificators = 15;
    [SerializeField] public int NumberDevilModificators = 15;
       
    [SerializeField] public float BiggerityMin = 0.3f;
    [SerializeField] public float BiggerityMax = 2.3f;

    HashSet<int> indicesForAngelModificators;
    HashSet<int> indicesForDevilModificators;

    List<Enemy> enemies;
    List<int> walkingTypesToGenerate;

    [SerializeField] Enemy enemyStraight;
    [SerializeField] Enemy enemySlant;

    [SerializeField] EnemyModifier angelModifier;
    [SerializeField] EnemyModifier devilModifier;

    private int[] modificatorsTable;

    [SerializeField] public int level;
    [SerializeField] public int additionalHealthPerLevel = 44;
    [SerializeField] public float additionalAttackRatioPerLevel = 0.244f;

    void Start()
    {
        //ProcessGeneratingEnemies();
    }

    internal void SetLevel(int level)
    {
        this.level = level;
        UpdateGeneratParams();
    }

    int minNumberOfEnemiesReactingToTimeFactor = 3;
    float numberOfEnemiesReactingToTimeFactorPerLevelFactor = 8f;

    int minNumberOfEnemiesNoReactingToTimeFactor = 5;
    float numberOfEnemiesNoReactingToTimeFactorPerLevelFactor = 10f;

    private void UpdateGeneratParams()
    {
        NumberOfEnemiesReactingToTimeFactor = (int)(minNumberOfEnemiesReactingToTimeFactor + numberOfEnemiesReactingToTimeFactorPerLevelFactor * level);
        NumberOfEnemiesNoReactingToTimeFactor = (int)(minNumberOfEnemiesNoReactingToTimeFactor + numberOfEnemiesNoReactingToTimeFactorPerLevelFactor * level);
        int allEnemies = NumberOfEnemiesReactingToTimeFactor + NumberOfEnemiesNoReactingToTimeFactor;
        numberOfEnemiesWalkingStraight = UnityEngine.Random.Range(0, allEnemies);
        NumberAngelModificators = UnityEngine.Random.Range(0, allEnemies);
        NumberDevilModificators = UnityEngine.Random.Range(0, allEnemies);
    }

    public void ProcessGeneratingEnemies()
    {
        UpdateGeneratParams();
        HandleModificatorsAmount();
        GenerateModificatorsIndices();
        GenerateEnemies();
    }

    private void HandleModificatorsAmount()
    {
        allEnemiesAmount = NumberOfEnemiesReactingToTimeFactor + NumberOfEnemiesNoReactingToTimeFactor;

        if (NumberOfEnemiesReactingToTimeFactor < 0) NumberOfEnemiesReactingToTimeFactor = 0;
        if (NumberOfEnemiesNoReactingToTimeFactor < 0) NumberOfEnemiesNoReactingToTimeFactor = 0;

        if (NumberAngelModificators < 0) NumberAngelModificators = 0;
        if (NumberDevilModificators < 0) NumberDevilModificators = 0;
        if (NumberAngelModificators > allEnemiesAmount) NumberAngelModificators = allEnemiesAmount;
        if (NumberDevilModificators > allEnemiesAmount) NumberDevilModificators = allEnemiesAmount;

        if (numberOfEnemiesWalkingStraight < 0) numberOfEnemiesWalkingStraight = 0;
        if (numberOfEnemiesWalkingStraight > allEnemiesAmount) numberOfEnemiesWalkingStraight = allEnemiesAmount;

        numberOfEnemiesWalkingSlant = allEnemiesAmount - numberOfEnemiesWalkingStraight;
    }

    private void GenerateModificatorsIndices()
    {
       indicesForAngelModificators = DrawIntegers(NumberAngelModificators, allEnemiesAmount);
       indicesForDevilModificators = DrawIntegers(NumberDevilModificators, allEnemiesAmount);
        walkingTypesToGenerate = new List<int>();

       for (int i = 0; i <= numberOfEnemiesWalkingStraight; i++)
       {
            walkingTypesToGenerate.Add(1);
       }
       for (int i = 0; i < numberOfEnemiesWalkingSlant; i++)
       {
           walkingTypesToGenerate.Add(2);
       }
       
       Shuffle(walkingTypesToGenerate);
    }

    private HashSet<int> DrawIntegers(int integersToDraw, int maxRange)
    {
        HashSet<int> resultSet = new HashSet<int>();

        while (resultSet.Count < integersToDraw)
        {
            resultSet.Add(UnityEngine.Random.Range(0, maxRange));
        }

        return resultSet;
    }

    private void GenerateEnemies()
    {
        Enemy prefab;
        for (int i = 0; i < walkingTypesToGenerate.Count; i++)
        {
            switch (walkingTypesToGenerate[i])
            {
                case 2:
                    prefab = enemySlant;
                    break;
                case 1:
                default:
                    prefab = enemyStraight;
                    break;
            }


            float xPos = UnityEngine.Random.Range(XLeftLimitInstantiate, XRightLimitInstantiate);
            float yPos = UnityEngine.Random.Range(YBottomLimitInstantiate, YUpperLimitInstantiate);

            if (i >= NumberOfEnemiesReactingToTimeFactor)
            {
                xPos += xOffsetForNoReactingForTimeFactorEnemies;
            }

            Enemy enemyInstance = Instantiate(prefab, new Vector2(xPos, yPos), Quaternion.identity) as Enemy;

            Health health = enemyInstance.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.addHealth(additionalHealthPerLevel * level);
            }

            EnemyAttacking enemyAttacking = enemyInstance.gameObject.GetComponent<EnemyAttacking>();
            if (enemyAttacking != null)
            {
                enemyAttacking.attackPowerFactor += (additionalAttackRatioPerLevel * level);
            }

            if (i < NumberOfEnemiesReactingToTimeFactor)
            {
                enemyInstance.GetComponent<EnemyTimeManagerReacting>().isReactingToFieldDefenderTimeFactor = true;
            }

            if (indicesForAngelModificators.Contains(i))
            {
                Instantiate(angelModifier, enemyInstance.transform.position, Quaternion.identity).transform.parent = enemyInstance.transform;
            }
            if (indicesForDevilModificators.Contains(i))
            {
                EnemyModifierDevil emd = Instantiate(devilModifier, enemyInstance.transform.position, Quaternion.identity) as EnemyModifierDevil;
                emd.transform.parent = enemyInstance.transform;
                if (prefab == enemySlant)
                {
                    emd.transform.localScale = new Vector2(0.5f, 0.5f);
                    emd.transform.localPosition = new Vector2(emd.transform.localPosition.x - 0.095f, transform.localPosition.y - 0.234f);
                }
            }

            float biggerityFactor = UnityEngine.Random.Range(BiggerityMin, BiggerityMax);
            enemyInstance.transform.localScale *= biggerityFactor;
        }
    }

    void Shuffle(List<int> integers)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < integers.Count; i++)
        {
            int tmp = integers[i];
            int r = UnityEngine.Random.Range(i, integers.Count - 1);
            integers[i] = integers[r];
            integers[r] = tmp;
        }
    }
}
