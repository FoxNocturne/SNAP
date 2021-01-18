using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Chapitre 1 - Niveau 1");
    }

    public void QuitGame()
    {
        Debug.Log("Exit the Game!");
        Application.Quit();
    }
    public void OpenWikiURL(string url)
    {
        Application.OpenURL("https://snaplejeu.fandom.com/fr/wiki/SNAP,_Le_Jeu");

    }

}
