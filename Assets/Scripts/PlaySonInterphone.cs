using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySonInterphone : MonoBehaviour
{
    AudioSource sonInterphoneEnclencher;
    public AudioClip[] sonInterphone;

    private void Start()
    {
        sonInterphoneEnclencher = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            sonInterphoneEnclencher.PlayOneShot(sonInterphone[0], 1f);


        }
    }
}
