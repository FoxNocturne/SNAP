using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteBouclier : MonoBehaviour
{
    public GameObject cameras;
    AudioSource sonBouclierChute;
    public AudioClip[] sonBouclier;

    private void Start()
    {
        sonBouclierChute = GetComponent<AudioSource>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && collision.gameObject.GetComponent<Hero>().onTheGround)
        {
            cameras.GetComponent<CameraFollowing>().EvenementChuteBouclier(transform);
            sonBouclierChute.PlayOneShot(sonBouclier[0], 0.1f);

        }
        
            
            
            
        
           
    }
}
