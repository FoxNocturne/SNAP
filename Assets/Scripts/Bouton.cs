﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouton : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées au bouton

    private bool actif = false;
    private bool playerOn = false;

    private void Update()
    {
        if(Input.GetButtonDown("Attraper") && playerOn)
            actif = !actif;

        if (actif)
            foreach (var objet in objetsRelies)
                objet.Activation();
        else
            foreach (var objet in objetsRelies)
                objet.Desactivation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerOn = false;
    }
}