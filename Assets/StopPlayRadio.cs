using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayRadio : MonoBehaviour
{
    AudioSource sonRadioCivilEnclencher;
    public AudioClip[] sonRadioCivil;

    private void Start()
    {
        sonRadioCivilEnclencher = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player"  )
        {
            //sonRadioCivilEnclencher.PlayOneShot(sonRadioCivil[0], 1f);
            sonRadioCivilEnclencher.Pause();

        }
    }
}
