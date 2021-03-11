using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées à la plaque

    [Header("Sprites")]
    public Sprite haut;
    public Sprite bas;

    AudioSource sonPlaqueEnclencher;
    public AudioClip[] sonPlaque;

    private List<GameObject> activateurs = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Item" || collision.tag=="Bouclier")
        {
            GetComponent<SpriteRenderer>().sprite = bas;
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
            if (activateurs.Count == 0)
                GetComponent<SpriteRenderer>().sprite = haut;

            foreach (var objet in objetsRelies)
                objet.Desactivation(gameObject);
        }
    }
}
