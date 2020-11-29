
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : ObjetActivable
{
    public float speed = 10f;      // Vitesse d'ouverture des portes
    [Range(0, 1)]
    public float ouverture = 0.9f; // Pourcentage d'ouverture des portes

    private Vector2 startingPos;
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        targetPos = new Vector2(transform.position.x, transform.position.y + GetComponent<BoxCollider2D>().size.y * transform.localScale.y * ouverture);
    }

    public override void Activation()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public override void Desactivation()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPos, speed * Time.deltaTime);
    }
}
