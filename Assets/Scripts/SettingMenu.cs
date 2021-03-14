using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionUI;
    public GameObject menuUI;
    public GameObject optionButtonMenu;

    public static float volume;
    public Slider volumeSelect;

    public void Update()
    {
       
        if ((PauseMenu.optionIsActived || MainMenu.optionUIisActived) && Input.GetButtonDown("Dash"))
        {
            optionUI.SetActive(false);
            menuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionButtonMenu);
        }




    }
    public void SetVolume ()
    {
        volume = volumeSelect.value;
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
