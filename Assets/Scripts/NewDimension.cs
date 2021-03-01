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
        if (Input.GetMouseButtonDown(0))
        {
            transition.SetTrigger("END");
           
        }
    }
}
