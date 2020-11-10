using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public bool tutoriel = false;

    private GameObject[] cameras = new GameObject[3];
    private float snapPressed;
    private int actualDimension = 0;

    // Character layer 8
    //
    // Dictature layer 9
    // Chaos layer     10
    // PostApo layer   11 

    void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        cameras[1].GetComponent<Camera>().enabled = false;
        cameras[2].GetComponent<Camera>().enabled = false;

        Physics2D.IgnoreLayerCollision(8, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 11);
    }

    void Update()
    {
        if (tutoriel)
            SnapTutoriel();
        else
            SnapNormal();
    }

    private void SnapTutoriel()
    {
        if(transform.position.x > 33)
        {
            //snapPressed vaudra -1 avec le bouton '1' et 1 avec le bouton '2'
            snapPressed = Input.GetAxis("SNAP");
            if (Input.GetButtonDown("SNAP"))
            {
                // Désactivation de la dimension actuelle
                cameras[actualDimension].GetComponent<Camera>().enabled = false;
                Physics2D.IgnoreLayerCollision(8, actualDimension + 9);

                // Changement d'index
                actualDimension = (actualDimension == 0 ? 2 : 0);

                // Activation de la nouvelle dimension
                cameras[actualDimension].GetComponent<Camera>().enabled = true;
                Physics2D.IgnoreLayerCollision(8, actualDimension + 9, false);
            }
        }
    }

    private void SnapNormal()
    {
        //snapPressed vaudra -1 avec le bouton '1' et 1 avec le bouton '2'
        snapPressed = Input.GetAxis("SNAP");
        if (Input.GetButtonDown("SNAP"))
        {
            ActiveSnap(snapPressed);
        }
    }

    public void ActiveSnap(float target)
    {
        Debug.Log(target == 1 ? "On recule" : "On avance");

        // Désactivation de la dimension actuelle
        cameras[actualDimension].GetComponent<Camera>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, actualDimension + 9);

        // Changement d'index
        actualDimension = (actualDimension + (target == 1 ? 1 : 2)) % 3;

        // Activation de la nouvelle dimension
        cameras[actualDimension].GetComponent<Camera>().enabled = true;
        Physics2D.IgnoreLayerCollision(8, actualDimension + 9, false);
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
}
