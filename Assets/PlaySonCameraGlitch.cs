using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySonCameraGlitch : MonoBehaviour
{
    AudioSource sonCameraEnclencher;
    public AudioClip[] sonCamera;


    void Start()
    {
        sonCameraEnclencher = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            sonCameraEnclencher.PlayOneShot(sonCamera[0], 1f);


        }
    }

    // Update is called once per frame
  
}
