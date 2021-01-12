using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées à la plaque

    AudioSource sonPlaqueEnclencher;
    public AudioClip[] sonPlaque;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Item")
            foreach (var objet in objetsRelies)
                objet.Activation(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Item")
            foreach (var objet in objetsRelies)
                objet.Desactivation(gameObject);
    }
}
