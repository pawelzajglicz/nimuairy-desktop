using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWallDefenderSetter : MonoBehaviour
{
    
    [SerializeField] public WallDefender wallDefender;
    [SerializeField] public WallDefenderPlaceHolder placeholder;



    public void SetDefenderPlaceholder(WallDefenderPlaceHolder placeholder)
    {
        this.placeholder = placeholder;
        this.wallDefender = placeholder.wallDefender;
        transform.position = placeholder.transform.position;
    }
}
