using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{

    [SerializeField] AudioClip buttonSound;
    [SerializeField] float buttonSoundVolume = 0.5f;

    GameManager gameManager;
    FieldDefenderMovement fieldDefenderMovement;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fieldDefenderMovement = FindObjectOfType<FieldDefenderMovement>();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fieldDefenderMovement.ResetSpeed();
            gameManager.StartBattle();
            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);
        }
    }
}
