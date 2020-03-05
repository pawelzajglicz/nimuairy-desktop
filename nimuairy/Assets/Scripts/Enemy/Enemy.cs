using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float biggerity = 1;
    [SerializeField] float biggerityModificatorMinRange = 0;
    [SerializeField] float biggerityModificatorMaxRange = 3;

    private void Awake()
    {
        biggerity += Random.Range(biggerityModificatorMinRange, biggerityModificatorMaxRange);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
