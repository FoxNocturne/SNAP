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
    public GameObject MomentLectureUI;       
    public bool CollectableInstance = false;
    public Button[] collectableButton;
    public TMPro.TMP_Text[] collectableName;
    public string[] nameCollec;
    public Sprite[] ImageCollec;
    public Image ImageAffiche;
    public Text TextDescription;
    public string[] description;
    List<int> ordre = new List<int>();

    private void Awake()
    {
        PlayerPrefs.DeleteAll(); // A supprimer une fois le test des collectables effectués
        ImageAffiche.color = new Color(0, 0, 0, 0);
        TextDescription.text = "";


            /*    Navigation navigation = new Navigation();        
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnUp = collectableButton[2];
                navigation.selectOnDown = collectableButton[3];
                collectableButton[1].navigation = navigation; 
                */ 

        
    }
    void Update()
    {
        if (PauseMenu.collectableUIisActtived && Input.GetButtonDown("Dash"))
        {
            
            if(MomentLectureUI.activeSelf == true)
            {
                MomentLectureUI.SetActive(false);
            }
            else
            {
                if(!CollectableInstance)
                {
                    menuUI.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(collectableButtonMenu);                
                }
                else
                {
                    CollectableInstance = false;
                // menuUI.GetComponent<PauseMenu>().CollectableInstance = false;
                    Time.timeScale = 1; 
                }
                collectableUI.SetActive(false);
            }


        }



    }


    public void UpdateCollectables()
    {
        ordre.Capacity = 0;
        for (int i = 1; i < collectableButton.Length; i++)
        {
            if (PlayerPrefs.GetInt(nameCollec[i]) != 0)
            {
                ordre.Add(i);
                Debug.Log(ordre[0]);
                collectableName[i].text = nameCollec[i];
                collectableButton[i].enabled = true;
                collectableButton[i].interactable = true;
            }
        } 
        Debug.Log(ordre.Count);
       if(ordre.Count == 1)
        {
                Navigation navigation = new Navigation();        
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnUp = collectableButton[ordre[0]];
                navigation.selectOnDown = collectableButton[ordre[0]];
                collectableButton[ordre[0]].navigation = navigation;            
        }
        else
        {
            for (int i = 0; i < ordre.Count; i++)
            {
                    Navigation navigation = new Navigation();        
                    navigation.mode = Navigation.Mode.Explicit;
                    if(i == 0)
                    {
                        navigation.selectOnUp = collectableButton[ordre[ordre.Count - 1]];
                    }
                    else
                    {
                        navigation.selectOnUp = collectableButton[ordre[i + 1]];
                    }
                    if(i == ordre.Count)
                    {
                        navigation.selectOnDown = collectableButton[ordre[0]];
                        
                    }
                    else
                    {
                        navigation.selectOnDown = collectableButton[ordre[i - 1]];
                    }
                    

                    collectableButton[ordre[i]].navigation = navigation;
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




