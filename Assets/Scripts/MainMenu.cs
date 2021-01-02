﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Chapitre 1 - Niveau 1 copie");
    }

    public void QuitGame()
    {
        Debug.Log("Exit the Game!");
        Application.Quit();
    }

}
