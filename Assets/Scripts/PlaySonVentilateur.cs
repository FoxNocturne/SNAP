﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySonVentilateur : MonoBehaviour
{
    AudioSource sonVentiloEnclencher;
    public AudioClip[] sonVentilo;

    //public GameObject player;

    void Start()
    {
        sonVentiloEnclencher = GetComponent<AudioSource>();
    }
    public void Sonventilo()
    {
        sonVentiloEnclencher.Play();
    }
    void Update()
    {
        
    }
}