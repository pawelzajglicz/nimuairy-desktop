using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefenderPlaceHolder : MonoBehaviour
{
    [SerializeField] public WallDefender wallDefender;
    [SerializeField] SelectedWallDefenderSetter setter;
    

    void Start()
    {
        wallDefender.placeholder = this;
        wallDefender.transform.position = transform.position;
        setter = FindObjectOfType<SelectedWallDefenderSetter>();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setter.SetDefenderPlaceholder(this);
        }
    }

    /*public void SetDefender(WallDefender wallDefender)
    {
        this.wallDefender = wallDefender;
        wallDefender.transform.position = transform.position;
        wallDefender.placeholder = this;
        wallDefender.isActive = false;
    }*/
}
