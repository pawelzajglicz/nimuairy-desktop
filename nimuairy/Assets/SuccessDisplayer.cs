using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessDisplayer : MonoBehaviour
{
    float waitingToReturnToCityTime = 4f;

    void Start()
    {
    }

    public void CelebrateSuccess()
    {
        StartCoroutine(ProcessReturnToCity());
    }

    protected IEnumerator ProcessReturnToCity()
    {
        yield return new WaitForSeconds(waitingToReturnToCityTime);
        CameraPositioner.GoToCityScreen();
        this.gameObject.SetActive(false);
    }


}
