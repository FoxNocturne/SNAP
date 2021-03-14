using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{
    public GameObject Credits;
    public GameObject MusiqueEnd;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Credits.activeSelf == true)
            {
                SceneManager.LoadSceneAsync("Menu");
            }
            Credits.SetActive(true);
            MusiqueEnd.SetActive(true);
        }
    }
}
