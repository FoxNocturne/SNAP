﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snap : MonoBehaviour
{
    public GameObject camerasParent;
    public GameObject UISnap;
    
    private Image[] interfaceSnap = new Image[6];
    

    [Header("Options")]
    public bool tutoriel = true;
    public bool niveau1  = true;

    [HideInInspector]
    public int dimensionAIgnorer = 0;
    [HideInInspector]
    public bool cantSnap = false;

    private List<GameObject> cameras = new List<GameObject>();
    private float snapPressed, demiTailleX, demiTailleY;
    private int actualDimension = 0;
    Animator anim;

    AudioSource soundSnap;
    public AudioClip[] sonSnap;

    public GameObject feedback;
    public Sprite[] bulle;


    // Character layer 8
    //
    // Dictature layer 9
    // Chaos layer     10
    // PostApo layer   11 

    void Start()
    {
        cameras.Add(camerasParent.transform.GetChild(0).gameObject);
        cameras.Add(camerasParent.transform.GetChild(1).gameObject);
        cameras.Add(camerasParent.transform.GetChild(2).gameObject);

        cameras[1].GetComponent<Camera>().enabled = false;
        cameras[2].GetComponent<Camera>().enabled = false;

        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(8, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 11);

        demiTailleX = (transform.localScale.x * GetComponent<BoxCollider2D>().size.x) / 2;
        demiTailleY = (transform.localScale.y * GetComponent<BoxCollider2D>().size.y) / 2;

        soundSnap = GetComponent<AudioSource>();

        

        // UI du snap
        //   Left -> actualDimension
        //   Right -> actualDimension + 3
        if(niveau1)
        {
            interfaceSnap[0] = UISnap.transform.GetChild(0).GetComponent<Image>();
            interfaceSnap[1] = UISnap.transform.GetChild(1).GetComponent<Image>();

            interfaceSnap[0].enabled = false;

            return;
        }

        interfaceSnap[0] = UISnap.transform.GetChild(0).GetComponent<Image>();
        interfaceSnap[1] = UISnap.transform.GetChild(2).GetComponent<Image>();
        interfaceSnap[2] = UISnap.transform.GetChild(4).GetComponent<Image>();
        interfaceSnap[3] = UISnap.transform.GetChild(1).GetComponent<Image>();
        interfaceSnap[4] = UISnap.transform.GetChild(3).GetComponent<Image>();
        interfaceSnap[5] = UISnap.transform.GetChild(5).GetComponent<Image>();

        interfaceSnap[0].enabled = false;
        interfaceSnap[2].enabled = false;
        interfaceSnap[3].enabled = false;
        interfaceSnap[4].enabled = false;



    }

    void Update()
    {
        if (cantSnap)
       
            return;
           

        if (tutoriel)
            return;

        if(niveau1)
            SnapNiveau1();
        else
            SnapNormal();

        GetComponent<PlacementPortail>().SetTargetDimension();

        
        if (Hero.flipLeft == true)
        {
            feedback.GetComponent<SpriteRenderer>().sprite = bulle[1];
            feedback.transform.position = Vector3.MoveTowards(new Vector3(transform.position.x - 3.2f, transform.position.y + 2.5f, transform.position.z), feedback.transform.position, 15 * Time.deltaTime);
        }
        else
        {
            feedback.GetComponent<SpriteRenderer>().sprite = bulle[0];
            feedback.transform.position = Vector3.MoveTowards(new Vector3(transform.position.x + 3.2f, transform.position.y + 2.5f, transform.position.z), feedback.transform.position, 15 * Time.deltaTime);
        }

    }

    private void SnapNiveau1()
    {
        //snapPressed vaudra -1 avec le bouton '1' et 1 avec le bouton '2'
        snapPressed = Input.GetAxis("SNAP");
        if (Input.GetButtonDown("SNAP"))
        {
            anim.SetTrigger("SNAP");
            if (Physics2D.OverlapArea(new Vector2(transform.position.x - demiTailleX + 0.1f, transform.position.y - demiTailleY + 0.1f), new Vector2(transform.position.x + demiTailleX - 0.1f, transform.position.y + demiTailleY - 0.1f),LayerMask.GetMask(LayerMask.LayerToName((actualDimension == 0 ? 2 : 0) + 9))))
            {
                
                feedback.SetActive(true);
                Invoke("CloseFeedback", 3f);
                return;
            }
            else
            {
                feedback.SetActive(false);
                soundSnap.PlayOneShot(sonSnap[0], 0.05f);
            }
           

            // Désactivation de la dimension actuelle
            cameras[actualDimension].GetComponent<Camera>().enabled = false;
            Physics2D.IgnoreLayerCollision(8, actualDimension + 9);

            // Changement d'index
            actualDimension = (actualDimension == 0 ? 2 : 0);

            // Activation de la nouvelle dimension
            cameras[actualDimension].GetComponent<Camera>().enabled = true;
            Physics2D.IgnoreLayerCollision(8, actualDimension + 9, false);
            GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID($"{LayerMask.LayerToName(actualDimension + 9)}Player");

            interfaceSnap[0].enabled = !interfaceSnap[0].isActiveAndEnabled;
            interfaceSnap[1].enabled = !interfaceSnap[1].isActiveAndEnabled;
        }
    }

    private void SnapNormal()
    {
        //snapPressed vaudra -1 avec le bouton '1' et 1 avec le bouton '2'
        snapPressed = Input.GetAxis("SNAP");
        if (Input.GetButtonDown("SNAP"))
        {
            anim.SetTrigger("SNAP");
            ActiveSnap(snapPressed);
        }
    }

    public void ActiveSnap(float target)
    {
        if (Physics2D.OverlapArea(new Vector2(transform.position.x - demiTailleX + 0.1f, transform.position.y - demiTailleY + 0.1f),
                                      new Vector2(transform.position.x + demiTailleX - 0.1f, transform.position.y + demiTailleY - 0.1f),
                                      LayerMask.GetMask(LayerMask.LayerToName(((actualDimension + (target == 1 ? 1 : 2)) % 3) + 9))))
        {
            feedback.SetActive(true);
            Invoke("CloseFeedback", 3f);
            return;
        }
        else
        {
            feedback.SetActive(false);
            soundSnap.PlayOneShot(sonSnap[0], 0.05f);
        }
       

        // Désactivation de la dimension actuelle
        cameras[actualDimension].GetComponent<Camera>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, actualDimension + 9);
        interfaceSnap[GetPreviousDimension() + 3].enabled = false;
        interfaceSnap[GetNextDimension()].enabled = false;

        // Changement d'index
        actualDimension = (actualDimension + (target == 1 ? 1 : 2)) % 3;

        // Activation de la nouvelle dimension
        cameras[actualDimension].GetComponent<Camera>().enabled = true;
        Physics2D.IgnoreLayerCollision(8, actualDimension + 9, false);
        GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID($"{LayerMask.LayerToName(actualDimension + 9)}Player");
        interfaceSnap[GetPreviousDimension() + 3].enabled = true;
        interfaceSnap[GetNextDimension()].enabled = true;

        // Verification des layers de transition
        IgnorerTransition(actualDimension == dimensionAIgnorer);
    }

    public int GetActualDimension()
    {
        return actualDimension;
    }

    public int GetPreviousDimension()
    {
        return (actualDimension + 2) % 3;
    }

    public int GetNextDimension()
    {
        return (actualDimension + 1) % 3;
    }

    public void SetActualDimension(int _actualDimension)
    {
        actualDimension = _actualDimension;
    }

    private void IgnorerTransition(bool param)
    {
        Physics2D.IgnoreLayerCollision(8, 12, param);
        Physics2D.IgnoreLayerCollision(8, 13, param);
        Physics2D.IgnoreLayerCollision(8, 14, param);
    }
    private void CloseFeedback()
    {

        feedback.SetActive(false);
    }

}
