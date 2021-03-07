using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDimension : MonoBehaviour
{
    public GameObject newDimensinUI;
    public Animator transition;

    void Start()
    {
        transition = GetComponent<Animator>();
    }


    void Update()
    {
        if (/*Input.GetButtonDown("SNAP")|| Input.GetButtonDown("Sauter")|| Input.GetButtonDown("Attraper")|| Input.GetButtonDown("Dash")||*/ Input.anyKeyDown)
        {
            transition.SetTrigger("END");
           
        }
    }
}
