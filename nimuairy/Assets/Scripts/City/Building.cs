using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected int level = 1;

    [SerializeField] protected TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        FindText();
    }

    private void FindText()
    {
        foreach (Transform child in transform)
        {
            TextMeshPro textComponent = child.GetComponent<TextMeshPro>();

            if (textComponent != null)
            {
                text = textComponent;
                break;
            }
        }
    }

    virtual protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
