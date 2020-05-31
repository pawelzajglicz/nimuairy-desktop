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
        FindObjectOfType<TopBar>().transform.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<SkillBar>().transform.localScale = new Vector3(0, 0, 0);
        CameraPositioner.GoToCityScreen();
        this.gameObject.SetActive(false);
    }


}
