using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ClignotementObjetInteractif : MonoBehaviour
{
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
