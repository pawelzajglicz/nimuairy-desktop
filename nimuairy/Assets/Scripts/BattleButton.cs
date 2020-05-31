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
            FindObjectOfType<TopBar>().transform.localScale = new Vector3(1, 1, 1);
            FindObjectOfType<SkillBar>().transform.localScale = new Vector3(1, 1, 1);
            FindObjectOfType<EnemiesGenerator>().ProcessGeneratingEnemies();
            FindObjectOfType<GameManager>().CountEnemies();
        }
    }
}
