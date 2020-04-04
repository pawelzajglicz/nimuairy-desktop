using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{

    private int paramValue = 10;
    public int ParamValue
    {
        get { return paramValue; }
        set
        {
            this.paramValue = value;
            FindObjectOfType<GameManager>().UpdateParamizables();
        }
    }
}

