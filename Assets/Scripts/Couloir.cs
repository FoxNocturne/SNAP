using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Couloir : MonoBehaviour
{
    public float speed = 5;
    public Light2D lightExt;
    public Light2D[] lightInt = new Light2D[2];

    private bool playerIn = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerIn)
        {
            lightExt.enabled = false;
            lightInt[0].enabled = true;
            lightInt[1].enabled = true;

            sr.color = new Color(255, 255, 255, Mathf.Clamp(sr.color.a + speed * Time.deltaTime, 0.5f, 1));
        }else if(!playerIn)
        {
            lightExt.enabled = true;
            lightInt[0].enabled = false;
            lightInt[1].enabled = false;

            sr.color = new Color(255, 255, 255, Mathf.Clamp(sr.color.a - speed * Time.deltaTime, 0.5f, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerIn = false;
    }
}
