using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{

    public float paramValue = 10f;
    [SerializeField] public float ParamValue
    {
        get { return paramValue; }
        set
        {
            this.paramValue = value;
            FindObjectOfType<GameManager>().UpdateParamizables();
        }
    }
}

