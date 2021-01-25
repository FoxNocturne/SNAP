using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySonCamion : MonoBehaviour
{
    AudioSource sonCamionEnclencher;
    public AudioClip[] sonCamion;

    private void Start()
    {
        sonCamionEnclencher = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DeadZones
        if (collision.tag == "Player")
        {
            sonCamionEnclencher.PlayOneShot(sonCamion[0], 1f);


        }
    }

}
