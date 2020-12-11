using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGoutte : MonoBehaviour
{
    AudioSource sonGoutteEnclencher;
    public AudioClip[] sonGoutte;

    private void Start()
    {
        sonGoutteEnclencher = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            sonGoutteEnclencher.PlayOneShot(sonGoutte[0], 1f);


        }
    }
}
