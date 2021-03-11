using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BoutonLigh : MonoBehaviour
{
    public Light2D light1;
    public Light2D light2;

    public Transform porte;
    private Vector2 initPos;

    void Start() => initPos = porte.position;

    void Update()
    {
        if((Vector2) porte.position != initPos)
        {
            light1.enabled = !GetComponent<Bouton>().actif;
            light2.enabled = !GetComponent<Bouton>().actif;
        }
        else
        {
            light1.enabled = false;
            light2.enabled = false;
        }
    }
}
