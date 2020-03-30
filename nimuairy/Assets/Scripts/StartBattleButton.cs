using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameManager.StartBattle();
        }
    }
}
