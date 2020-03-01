using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public Vector2 FindTarget();

    public Vector2 GetDefaultTarget()
    {
        return Camera.main.transform.position;
    }
}
