using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    private GameObject[] cameras = new GameObject[3];
    private float snapPressed;
    private int actualDimension = 0;

    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        cameras[1].GetComponent<Camera>().enabled = false;
        cameras[2].GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //snapPressed vaudra -1 avec le bouton '1' et 1 avec le bouton '2'
        snapPressed = Input.GetAxis("SNAP");
        if(Input.GetButtonDown("SNAP"))
        {
            Debug.Log(snapPressed);
        }
    }
}
