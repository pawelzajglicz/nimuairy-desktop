using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{

     public void OnMouseOver()
    {
        Debug.Log("1");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("2");
            CameraPositioner.GoToBattleScreen();
        }
    }
}
