﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouton : MonoBehaviour
{
    public List<ObjetActivable> objetsRelies; // Liste des objets connectées au bouton

    private bool actif = false;
    private bool playerOn = false;

  
    AudioSource sonBouttonEnclencher;
    public AudioClip[] sonBoutton;

    private void Start()
    {
        sonBouttonEnclencher = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Attraper") && playerOn)
            actif = !actif;

        if (actif)
        {
            foreach (var objet in objetsRelies)
            {
                objet.Activation();
                sonBouttonEnclencher.PlayOneShot(sonBoutton[0], 1f);
            }

        }
                
                
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
