using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectablesUI : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject collectableUI;
    public GameObject collectableButtonMenu;

    public Button[] collectableButton;
    public TMPro.TMP_Text[] collectableName;
    public string[] nameCollec;
    public Sprite[] ImageCollec;
    public Image ImageAffiche;
    public Text TextDescription;
    public string[] description;

    private void Awake()
    {
        PlayerPrefs.DeleteAll(); // A supprimer une fois le test des collectables effectués
        ImageAffiche.color = new Color(0, 0, 0, 0);
        TextDescription.text = "";
         
        for (int i = 1; i <= 12; i++)
        {
            PlayerPrefs.SetInt(nameCollec[i], i);
        } 

    }
    void Update()
    {
        if ((PauseMenu.collectableUIisActtived || MainMenu.collectableUIisActtived) && Input.GetButtonDown("Dash"))
        {
            collectableUI.SetActive(false);
            menuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(collectableButtonMenu);
        }


        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.GetInt(nameCollec[i]) != 0)
            {

                collectableName[i].text = nameCollec[i];
                collectableButton[i].enabled = true;
                collectableButton[i].interactable = true;
            }
        }
    }

    public void TaskForDisplay(int value)
    {
        Debug.Log("Ok !");
        ImageAffiche.sprite = ImageCollec[value];
        TextDescription.text = description[value];
        ImageAffiche.color = new Color(1, 1, 1, 1);
    }


}




