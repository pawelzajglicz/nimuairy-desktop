using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraPositioner
{
    static Vector3 battlePosition = new Vector3(0, 0, -10);
    static Vector3 cityPosition = new Vector3(-50, 0, -10);

    public static void GoToBattleScreen()
    {
        Camera.main.transform.position = battlePosition;
    }

    public static void GoToCityScreen()
    {
        Camera.main.transform.position = cityPosition;
    }
}
