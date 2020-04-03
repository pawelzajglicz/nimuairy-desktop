using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnDestroy()
    {
        FindObjectOfType<GameManager>().ProcessDefenceFailure();
    }
}
