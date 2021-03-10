using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : ObjetActivable
{
    public Color couleurEteint;
    public Color couleurAllume;


    private void Update()
    {
        if (activators.Count != 0)
            GetComponent<SpriteRenderer>().color = couleurAllume;
        else
            GetComponent<SpriteRenderer>().color = couleurEteint;
    }

    public override void Activation(GameObject activator)
    {
        activators.Add(activator);
        //sonPorteEnclencher.PlayOneShot(sonPorte[0], 1f);
    }

    public override void Desactivation(GameObject activator)
    {
        activators.Remove(activator);
        //sonPorteEnclencher.PlayOneShot(sonPorte[1], 1f);
    }
}
