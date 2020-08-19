using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{

    [SerializeField] AudioClip buttonSound;
    [SerializeField] float buttonSoundVolume = 0.5f;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CameraPositioner.GoToCredits();

            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);
        }
    }
}
