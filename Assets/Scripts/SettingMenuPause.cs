using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingMenuPause : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionUI;
    public GameObject menuUI;
    public GameObject optionButtonMenu;

    Camera[] Camera = new Camera[3]; // 0 = Dictature // 1 = Chaos // 2 = Post-Apo

    AudioSource[] music = new AudioSource[3];
    
    public static float volume;
    public Slider volumeSelect;

    public GameObject InstantLecture;


    void Start()
    {
        Camera[0] = GameObject.Find("Cameras/Dictature (Main)").GetComponent<Camera>();
        Camera[1] = GameObject.Find("Cameras/Chaos").GetComponent<Camera>();
        Camera[2] = GameObject.Find("Cameras/PostApo").GetComponent<Camera>();

        music[0] = GameObject.Find("Themes Musique/DictatureMusique").GetComponent<AudioSource>();
        music[1] = GameObject.Find("Themes Musique/Chaos").GetComponent<AudioSource>();
        music[2] = GameObject.Find("Themes Musique/Post-ApoMusique").GetComponent<AudioSource>();
        volumeSelect.value = 0.3f;
        SetVolume();
    }

  

    public void Update()
    {
        
        if (PauseMenu.optionIsActived && Input.GetButtonDown("Dash"))
        {
            optionUI.SetActive(false);
            menuUI.SetActive(true);
            Time.timeScale = 0f;
            PauseMenu.optionIsActived = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionButtonMenu);
        }




    }
    public void SetVolume ()
    {
        
        if(!InstantLecture.activeSelf)
        {
            volume = volumeSelect.value;
        }
        
        for(int i = 0; i < 3; i++)
        {
            if(Camera[i].enabled)
            {
                music[i].volume = volume;
            }
        }
        Debug.Log(volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
   
}
