using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteBouclier : MonoBehaviour
{
    public GameObject cameras;
    public GameObject Hero;
    public Animator BulleAnimator;
    AudioSource sonBouclierChute;
    public AudioClip[] sonBouclier;

    private bool PlayerUp = false;

    private void Start()
    {
        sonBouclierChute = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerUp && Input.GetButtonDown("Attraper") && Hero.GetComponent<Hero>().onTheGround && !Hero.GetComponent<Hero>().dash)
        {
            cameras.GetComponent<CameraFollowing>().EvenementChuteBouclier(transform);

            Hero.GetComponent<Animator>().SetBool("run", false);
            Hero.transform.position = new Vector2(67.77f, 8.51f);
            sonBouclierChute.PlayOneShot(sonBouclier[0], 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            PlayerUp = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            PlayerUp = false;
    }
}