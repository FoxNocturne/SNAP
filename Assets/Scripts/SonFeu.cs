using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFeu : MonoBehaviour
{
    AudioSource sonFeuEnclencher;
    //public AudioClip[] sonFeu;

    void Start()
    {
        sonFeuEnclencher = GetComponent<AudioSource>();
    }

     public void SonCrepitementFeu()
    {
        sonFeuEnclencher.Play();
    }
   
 
}
