﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{


    public void PrologueScreen()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial_Level");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
