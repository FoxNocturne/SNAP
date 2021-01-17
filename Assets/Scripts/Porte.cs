
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : ObjetActivable
{
    public float speed = 10f;      // Vitesse d'ouverture des portes
    [Range(-1, 1)]
    public float ouverture = 0.9f; // Pourcentage d'ouverture des portes

    private Vector2 startingPos;
    private Vector2 targetPos;
    //AudioSource sonPorteEnclencher;
    //public AudioClip[] sonPorte;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        targetPos = new Vector2(transform.position.x, transform.position.y + GetComponent<BoxCollider2D>().size.y * transform.localScale.y * ouverture);
        //sonPorteEnclencher = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (activators.Count != 0)
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, startingPos, speed * Time.deltaTime);
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
