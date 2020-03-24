using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBar : MonoBehaviour
{

    private float startXScale;
    [SerializeField] public float currentXScale;

    private void Awake()
    {
        startXScale = transform.localScale.x;
        currentXScale = startXScale;
    }

    public void SetPercentage(float percentage)
    {
        currentXScale = startXScale * percentage / 100;
        transform.localScale = new Vector2(currentXScale, transform.localScale.y);
    }
}
