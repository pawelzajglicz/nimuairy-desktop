using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraPositioner
{
    static Vector3 battlePosition = new Vector3(0, 0, -10);
    static Vector3 cityPosition = new Vector3(0, -50, -10);
    static Vector3 mainMenuPosition = new Vector3(0, 0, -10);
    static Vector3 tutorialPosition = new Vector3(0, -50, -10);

    public static void GoToBattleScreen()
    {
        Camera.main.transform.position = battlePosition;
    }

    public static void GoToCityScreen()
    {
        Camera.main.transform.position = cityPosition;
    }

    public static void GoToMainMenu()
    {
        Camera.main.transform.position = mainMenuPosition;
    }

    public static void GoToTutorial()
    {
        Camera.main.transform.position = tutorialPosition;
    }

}
