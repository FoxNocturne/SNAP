using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PassageNiveau2 : MonoBehaviour
{
    public GameObject Chargement;

    public Image BarreChargement;
    public Text textLoading;
    float chargementPourcent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        Chargement.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Niveau 2 -IntroPontGravité");
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
