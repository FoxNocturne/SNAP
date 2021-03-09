using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] UIButtons;
    public GameObject Logo;
    public GameObject Chargement;
    public GameObject optionUI;
    public GameObject collectableUI;
    public GameObject quitterBDUI;

    public GameObject menuUI;
    public GameObject quitterButton;
    public GameObject boiteDialogueUI;

    public GameObject firstOptionButton;
    public GameObject firstCollectableButton;
    public GameObject firstCQButton;
    
    public Image BarreChargement;
    public Text textLoading;
    float chargementPourcent;
    
    public void PlayGame()
    {
        StartCoroutine(LoadAsyncScene());
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
    public void NonBoiteDialogue()
    {
        boiteDialogueUI.SetActive(false);
        menuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitterButton);
    }

    IEnumerator LoadAsyncScene()
    {
        for(int i = 0; i < 5; i++)
        {
            UIButtons[i].SetActive(false);
        }
        Logo.SetActive(false);
        Chargement.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Chapitre 1 - Niveau 1");
        BarreChargement.fillAmount = 0;
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)               // .progress ==> moment la scène se charge : valeur [0; 0.9]
                                                // .isDone ==> activation de la scène : valeur [0.9; 1]
        {
            textLoading.text = "" + Mathf.Round(BarreChargement.fillAmount * 100) + "%";
            chargementPourcent = asyncLoad.progress / 0.9f;
            if(BarreChargement.fillAmount < chargementPourcent)
            {
                BarreChargement.fillAmount += Time.deltaTime;
            }
            if(Mathf.Round(BarreChargement.fillAmount * 100) >= 100)
            {
                yield return new WaitForSeconds(3);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;

        }
    }

}
