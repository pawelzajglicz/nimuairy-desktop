using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{

    abstract public GameObject FindTarget();
    [SerializeField] public WallDefender wallDefender;


    virtual public Vector2 FindTargetPosition() { return GetDefaultTarget(); }


    private void Start()
    {
        wallDefender = GetComponent<WallDefender>();
    }

    public Vector2 GetDefaultTarget()
    {
        return Camera.main.transform.position;
    }


}
