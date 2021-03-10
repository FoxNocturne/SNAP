using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biche : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Cameras" && collision.transform.GetChild(2).GetComponent<Camera>().enabled)
            GetComponent<Animator>().SetTrigger("Course");
    }
}
