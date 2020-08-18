using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewEnemiesButton : MonoBehaviour
{

    [SerializeField] AudioClip buttonSound;
    [SerializeField] float buttonSoundVolume = 0.5f;

    Vector3 destination = new Vector3(40, 0, -10);
    Vector3 cameraStartPosition;
    [SerializeField] float slidingTime = 5f;
    [SerializeField] float waitingToReturnTime = 2f;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LookAtBattlegroundForward());
            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);
        }
    }

    public IEnumerator LookAtBattlegroundForward()
    {
        cameraStartPosition = Camera.main.transform.position;

        float elapsedTime = 0;
        while (elapsedTime < slidingTime)
        {
            Camera.main.transform.position = Vector3.Lerp(cameraStartPosition, destination, (elapsedTime / slidingTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(WaitAtTheEnd());
    }

    public IEnumerator WaitAtTheEnd()
    {
        yield return new WaitForSeconds(waitingToReturnTime);

        StartCoroutine(LookAtBattlegrounBackward());
    }

    public IEnumerator LookAtBattlegrounBackward()
    {
        float elapsedTime = 0;
        while (elapsedTime < slidingTime)
        {
            Camera.main.transform.position = Vector3.Lerp(destination, cameraStartPosition, (elapsedTime / slidingTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
