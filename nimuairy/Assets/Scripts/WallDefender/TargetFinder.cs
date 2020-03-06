using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{

    virtual public Vector2 FindTargetPosition() { return GetDefaultTarget(); }
    abstract public GameObject FindTarget();

    public Vector2 GetDefaultTarget()
    {
        return Camera.main.transform.position;
    }
}
