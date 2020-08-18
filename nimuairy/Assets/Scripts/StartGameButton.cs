using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{

    [SerializeField] AudioClip buttonSound;
    [SerializeField] float buttonSoundVolume = 0.5f;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<LevelManager>().StartGame();

            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);
        }
    }
}
