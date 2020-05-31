using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    internal void QuitGame()
    {
        Application.Quit();
    }

    internal void RestartGame()
    {
        TimeManager.playerTimeFactor = 0;
        GoToMainMenu();
    }

    public void ResetGame()
    {
        TimeManager.playerTimeFactor = 0;
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        TimeManager.playerTimeFactor = 0;
        SceneManager.LoadScene(0);
    }
}
