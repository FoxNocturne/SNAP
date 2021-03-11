using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ClignotementObjetInteractif : MonoBehaviour
{
    void Start()
    {
        bulle.SetBool("PlayerNear", false);
    }
    public Animator bulle;
    void OnTriggerEnter2D(Collider2D collider)
    {
        bulle.SetBool("PlayerNear", true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        bulle.SetBool("PlayerNear", false);
    }    
}
