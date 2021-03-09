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

    public void Update()
    {
       
        if (PauseMenu.optionIsActived && Input.GetButtonDown("Dash"))
        {
            optionUI.SetActive(false);
            menuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionButtonMenu);
        }




    }
    public void SetVolume (float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
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
