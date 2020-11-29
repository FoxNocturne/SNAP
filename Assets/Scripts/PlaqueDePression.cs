﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées à la plaque

    private List<Collider2D> actualColliders = new List<Collider2D>();
    private int actualCollidersOffset;

    private void Start()
    {
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);
        actualCollidersOffset = actualColliders.Count;
    }

    private void Update()
    {
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);

        // La plaque de pression ne détecte pas les objets ou personnage qui lui marchent dessus
        // Elle détecte en réalité une différence de colliders sur elle
        if (actualColliders.Count - actualCollidersOffset == 0)
        {
            foreach (var objet in objetsRelies)
                objet.Desactivation();
        }
        else
        {
            foreach (var objet in objetsRelies)
                objet.Activation();
        }
    }
}