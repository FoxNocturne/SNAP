using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDimension : MonoBehaviour
{
    public GameObject newDimensinUI;
    public Animator transition;

    void Start()
    {
        Time.timeScale = 0f;

        transition = GetComponent<Animator>();
        transition.updateMode = AnimatorUpdateMode.UnscaledTime;
    }


    void Update()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;
            transition.SetTrigger("END");
           
        }
    }
}
