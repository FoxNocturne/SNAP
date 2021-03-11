using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeMusique : MonoBehaviour
{
    public AudioSource dictature;
    public AudioSource postApo;
    public AudioSource chaos;
    void Start()
    {
        dictature = GetComponent<AudioSource>();
        postApo = GetComponent<AudioSource>();
        chaos = GetComponent<AudioSource>();
    }


    void Update()
    {
        /*if(cameras[1].GetComponent<Camera>().enabled = false)
        {

        }*/
    }
}
