using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] float xLeftLimitInstantiate = -10f;
    [SerializeField] float xRightLimitInstantiate = 14f;
    [SerializeField] float yUpperLimitInstantiate = 3f;
    [SerializeField] float yBottomLimitInstantiate = -4f;

    [SerializeField] float xOffsetForNoReactingForTimeFactorEnemies = 26f;

    [SerializeField] int numberOfEnemiesReactingToTimeFactor = 18;
    [SerializeField] int numberOfEnemiesNoReactingToTimeFactor = 26;
    int allEnemiesAmount;

    [SerializeField] int numberOfEnemiesWalkingStraight = 26;
   /* [SerializeField]*/ int numberOfEnemiesWalkingSlant;

    [SerializeField] int numberAngelModificators = 15;
    [SerializeField] int numberDevilModificators = 15;
       
    [SerializeField] float biggerityMin = 0.3f;
    [SerializeField] float biggerityMax = 2.3f;

    HashSet<int> indicesForAngelModificators;
    HashSet<int> indicesForDevilModificators;

    List<Enemy> enemies;
    List<int> walkingTypesToGenerate;

    [SerializeField] Enemy enemyStraight;
    [SerializeField] Enemy enemySlant;

    [SerializeField] EnemyModifier angelModifier;
    [SerializeField] EnemyModifier devilModifier;

    private int[] modificatorsTable;

    void Start()
    {
       // ProcessGeneratingEnemies();
    }

    private void ProcessGeneratingEnemies()
    {
        HandleModificatorsAmount();
        GenerateModificatorsIndices();
        GenerateEnemies();
    }

    private void HandleModificatorsAmount()
    {
        allEnemiesAmount = numberOfEnemiesReactingToTimeFactor + numberOfEnemiesNoReactingToTimeFactor;

        if (numberOfEnemiesReactingToTimeFactor < 0) numberOfEnemiesReactingToTimeFactor = 0;
        if (numberOfEnemiesNoReactingToTimeFactor < 0) numberOfEnemiesNoReactingToTimeFactor = 0;

        if (numberAngelModificators < 0) numberAngelModificators = 0;
        if (numberDevilModificators < 0) numberDevilModificators = 0;
        if (numberAngelModificators > allEnemiesAmount) numberAngelModificators = allEnemiesAmount;
        if (numberDevilModificators > allEnemiesAmount) numberDevilModificators = allEnemiesAmount;

        if (numberOfEnemiesWalkingStraight < 0) numberOfEnemiesWalkingStraight = 0;
        if (numberOfEnemiesWalkingStraight > allEnemiesAmount) numberOfEnemiesWalkingStraight = allEnemiesAmount;

        numberOfEnemiesWalkingSlant = allEnemiesAmount - numberOfEnemiesWalkingStraight;
    }

    private void GenerateModificatorsIndices()
    {
       indicesForAngelModificators = DrawIntegers(numberAngelModificators, allEnemiesAmount);
       indicesForDevilModificators = DrawIntegers(numberDevilModificators, allEnemiesAmount);
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


            float xPos = UnityEngine.Random.Range(xLeftLimitInstantiate, xRightLimitInstantiate);
            float yPos = UnityEngine.Random.Range(yBottomLimitInstantiate, yUpperLimitInstantiate);

            if (i >= numberOfEnemiesReactingToTimeFactor)
            {
                xPos += xOffsetForNoReactingForTimeFactorEnemies;
            }

            Enemy enemyInstance = Instantiate(prefab, new Vector2(xPos, yPos), Quaternion.identity) as Enemy;

            if (i < numberOfEnemiesReactingToTimeFactor)
            {
                enemyInstance.GetComponent<EnemyTimeManagerReacting>().isReactingToFieldDefenderTimeFactor = true;
            }

            if (indicesForAngelModificators.Contains(i))
            {
                Instantiate(angelModifier, enemyInstance.transform.position, Quaternion.identity).transform.parent = enemyInstance.transform;
            }
            if (indicesForDevilModificators.Contains(i))
            {
                Instantiate(devilModifier, enemyInstance.transform.position, Quaternion.identity).transform.parent = enemyInstance.transform;
            }

            float biggerityFactor = UnityEngine.Random.Range(biggerityMin, biggerityMax);
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
