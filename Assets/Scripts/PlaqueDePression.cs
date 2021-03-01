using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées à la plaque

    AudioSource sonPlaqueEnclencher;
    public AudioClip[] sonPlaque;

    private List<GameObject> activateurs = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Item")
        {
            activateurs.Add(collision.gameObject);
            foreach (var objet in objetsRelies)
                objet.Activation(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(activateurs.Contains(collision.gameObject))
        {
            activateurs.Remove(collision.gameObject);
            foreach (var objet in objetsRelies)
                objet.Desactivation(gameObject);
        }
    }
}
