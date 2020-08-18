using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{

    [SerializeField] AudioClip buttonSound;
    [SerializeField] float buttonSoundVolume = 0.5f;

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
            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);
        }
    }
}
