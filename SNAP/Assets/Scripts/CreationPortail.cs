﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationPortail : MonoBehaviour
{
    public bool tutoriel = true;
    public bool niveau1 =  true;

    public GameObject portailPrefab;
    public Color[] portailColors = new Color[3];

    private GameObject portailActualDimension, portailTargetDimension;
    private Snap snapScript;

    void Start()
    {
        snapScript = GetComponent<Snap>();    
    }

    void Update()
    {
        if (tutoriel)
            return;

        float portailInput = Input.GetAxis("Portail");

        if (Input.GetButtonDown("Portail"))
        {
            if (niveau1)
                CreerPortailNiveau1();
            else
                CreerPortail(portailInput == 1 ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension());
        }
    }

    private void CreerPortailNiveau1()
    {
        if (portailActualDimension)
        {
            Destroy(portailActualDimension);
            Destroy(portailTargetDimension);
        }

        int actualDimension = snapScript.GetActualDimension(); ;
        int targetDimension = actualDimension == 0 ? 2 : 0;

        portailActualDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailActualDimension.GetComponent<SpriteRenderer>().color = portailColors[targetDimension];
        portailActualDimension.layer = actualDimension + 9;
        portailActualDimension.GetComponent<Portail>().targetDimension = targetDimension;

        portailTargetDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailTargetDimension.GetComponent<SpriteRenderer>().color = portailColors[actualDimension];
        portailTargetDimension.layer = targetDimension + 9;
        portailTargetDimension.GetComponent<Portail>().targetDimension = actualDimension;

        portailActualDimension.GetComponent<Portail>().portalLinked = portailTargetDimension;
        portailTargetDimension.GetComponent<Portail>().portalLinked = portailActualDimension;
    }

    private void CreerPortail(int dimensionCible)
    {
        if (portailActualDimension)
        {
            Destroy(portailActualDimension);
            Destroy(portailTargetDimension);
        }

        portailActualDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailActualDimension.GetComponent<SpriteRenderer>().color = portailColors[dimensionCible];
        portailActualDimension.layer = snapScript.GetActualDimension() + 9;
        portailActualDimension.GetComponent<Portail>().targetDimension = dimensionCible;
        
        portailTargetDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailTargetDimension.GetComponent<SpriteRenderer>().color = portailColors[snapScript.GetActualDimension()];
        portailTargetDimension.layer = dimensionCible + 9;
        portailTargetDimension.GetComponent<Portail>().targetDimension = snapScript.GetActualDimension();

        portailActualDimension.GetComponent<Portail>().portalLinked = portailTargetDimension;
        portailTargetDimension.GetComponent<Portail>().portalLinked = portailActualDimension;

        portailActualDimension.name = "Premier portail";
        portailTargetDimension.name = "Second portail";
    }
}
    