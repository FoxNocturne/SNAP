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
    public GameObject collectableSelect;
    public GameObject MomentLectureUI;
    public GameObject ZoomCollectableUI;
    public Image CollectableUI;
    public bool CollectableInstance = false;
    public Button[] collectableButton;
    public TMPro.TMP_Text[] collectableName;
    public string[] nameCollec;
    public Sprite[] ImageCollec;
    public Image ImageAffiche;
    public Text TextDescription;
    public string[] description;
    public AudioClip[] SonsLecture;
    public AudioClip Page;

    public Sprite[] AfficheLecture;
    public Image TextLecture;
    private int saveButton;


    private void Awake()
    {
        // PlayerPrefs.DeleteAll(); // A supprimer une fois le test des collectables effectués
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
        if ((PauseMenu.collectableUIisActtived || MainMenu.collectableUIisActtived) && Input.GetButtonDown("Dash"))
        {
            
            if(MomentLectureUI.activeSelf == true || ZoomCollectableUI.activeSelf == true)
            {
                MomentLectureUI.SetActive(false);
                ZoomCollectableUI.SetActive(false);
                collectableButton[saveButton].enabled = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(collectableSelect);                
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
        List<int> ordre = new List<int>();

        for (int i = 1; i < collectableButton.Length; i++)
        {
            if (PlayerPrefs.GetInt(nameCollec[i]) != 0)
            {
                ordre.Add(i);
                collectableName[i].text = nameCollec[i];
                collectableButton[i].enabled = true;
                collectableButton[i].interactable = true;
            }
        } 
        for (int i = 0; i < ordre.Count; i++)
        {
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;
            if(ordre.Count > 1)
            {
                if(i == 0)
                {
                    navigation.selectOnUp = collectableButton[ordre[ordre.Count - 1]];
                }
                else
                {
                    navigation.selectOnUp = collectableButton[ordre[i - 1]];
                }
                if(i == ordre.Count - 1)
                {
                    navigation.selectOnDown = collectableButton[ordre[0]]; 
                    
                                          
                }
                else
                {
                     navigation.selectOnDown = collectableButton[ordre[i + 1]];
                }
            }
            else
            {
                navigation.selectOnUp = collectableButton[ordre[0]];
                navigation.selectOnDown = collectableButton[ordre[0]];
            }
            collectableButton[ordre[i]].navigation = navigation;
        } 
    }
    public void TaskForDisplay(int value)
    {
        ImageAffiche.sprite = ImageCollec[value];
        TextDescription.text = description[value];
        ImageAffiche.color = new Color(1, 1, 1, 1);
    }

    public void DisplayMomentLecture(int value)
    {
        MomentLectureUI.SetActive(true);
        AudioSource Voix = MomentLectureUI.GetComponent<AudioSource>();
        Voix.PlayOneShot(Page, 1f);
        collectableSelect = collectableButton[value].transform.gameObject;
        collectableButton[value].enabled = false;        
        saveButton = value;
        switch(value)
        {
            case 5 :
            Voix.clip =  SonsLecture[0];
            TextLecture.sprite = AfficheLecture[0];
            Voix.Play();            
            break;

            case 6 :
            Voix.clip =  SonsLecture[1];
            TextLecture.sprite = AfficheLecture[1];
            Voix.Play();  
            break;

            case 7 :
            Voix.clip =  SonsLecture[2];
            TextLecture.sprite = AfficheLecture[2];
            Voix.Play();  
            break;

            case 8 :
            Voix.clip =  SonsLecture[3];
            TextLecture.sprite = AfficheLecture[3];
            Voix.Play();  
            break;  

            case 9 :
            Voix.clip =  SonsLecture[4];
            TextLecture.sprite = AfficheLecture[4];
            Voix.Play();  
            break;  

            case 10 :
            Voix.clip =  SonsLecture[5];
            TextLecture.sprite = AfficheLecture[5];
            Voix.Play();  
            break;              
        }
    }

    public void DisplayCollectable(int value)
    {
        ZoomCollectableUI.SetActive(true);
        CollectableUI.sprite = ImageCollec[value];
        collectableSelect = collectableButton[value].transform.gameObject;
        collectableButton[value].enabled = false;

        saveButton = value;
    }

}




