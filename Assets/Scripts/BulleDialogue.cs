using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulleDialogue : MonoBehaviour
{
    public Animator anim;
    public bool hasGlitchEffect = false;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
            anim.SetBool("PlayerNear", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            anim.SetBool("PlayerNear", false);
    }
}
