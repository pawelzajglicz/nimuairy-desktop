using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CameraPositioner.GoToBattleScreen();
            FindObjectOfType<EnemiesGenerator>().ProcessGeneratingEnemies();
            FindObjectOfType<GameManager>().CountEnemies();
        }
    }
}
