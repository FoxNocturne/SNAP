using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées à la plaque

    private List<Collider2D> actualColliders = new List<Collider2D>();
    private int actualCollidersOffset;

    AudioSource sonPlaqueEnclencher;
    public AudioClip[] sonPlaque;

    private void Start()
    {

        GetComponent<PolygonCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);
        if (actualColliders != null)
            actualCollidersOffset = actualColliders.Count;
        else
            actualCollidersOffset = 0;

    }

    private void Update()
    {
        GetComponent<PolygonCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);

        // La plaque de pression ne détecte pas les objets ou personnage qui lui marchent dessus
        // Elle détecte en réalité une différence de colliders sur elle
        if (actualColliders.Count - actualCollidersOffset == 0)
        {
            //sonPlaqueEnclencher.PlayOneShot(sonPlaque[0], 1f);

            foreach (var objet in objetsRelies)
            {
                objet.Desactivation();
                

            }
                
        }
        else
        {
            //sonPlaqueEnclencher.PlayOneShot(sonPlaque[0], 1f);
            foreach (var objet in objetsRelies)
            {
                
                objet.Activation();
                //sonPlaqueEnclencher.PlayOneShot(sonPlaque[0], 1f);

            }


        }
    }
}
