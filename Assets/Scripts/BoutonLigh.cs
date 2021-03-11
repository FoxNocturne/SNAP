using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BoutonLigh : MonoBehaviour
{
    public Light2D light1;
    public Light2D light2;

    void Update()
    {
        light1.enabled = GetComponent<Bouton>().actif;
        light2.enabled = GetComponent<Bouton>().actif;
    }
}
