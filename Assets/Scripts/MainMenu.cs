using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject optionUI;
    public GameObject collectableUI;
    public GameObject quitterBDUI;

    public GameObject firstOptionButton;
    public GameObject firstCollectableButton;
    public GameObject firstCQButton;
    
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
    public void CollectableMenu()
    {
        collectableUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCollectableButton);

    }
    public void OptionMenu()
    {
        optionUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionButton);
    }
    public void QuitterMenu()
    {
        quitterBDUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCQButton);
    }

}
