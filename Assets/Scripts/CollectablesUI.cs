using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesUI : MonoBehaviour
{
    public Button[] collectableButton;
    public TMPro.TMP_Text[] collectableName;
    public string[] nameCollec;
    public Sprite[] ImageCollec;
    public Image ImageAffiche;
    public Text TextDescription;
    public string[] description;


    private void Start()
    {
        ImageAffiche.color = new Color(0, 0, 0, 0);
        TextDescription.text = "";
        PlayerPrefs.DeleteAll();
        for (int i = 1; i <= 12; i++)
        {
            PlayerPrefs.SetInt(nameCollec[i], i);
        }

    }
    void Update()
    {       
        for(int i = 1; i <= 5; i++)
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
        ImageAffiche.sprite = ImageCollec[value];
        TextDescription.text = description[value];
        ImageAffiche.color = new Color(1, 1, 1, 1);
    }


}




