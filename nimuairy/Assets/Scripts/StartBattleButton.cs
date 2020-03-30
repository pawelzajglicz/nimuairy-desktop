using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.IsBattle = true;
        }
    }
}
