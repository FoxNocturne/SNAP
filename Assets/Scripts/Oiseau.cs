using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oiseau : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<Snap>().GetActualDimension() == 2)
            GetComponent<Animator>().SetTrigger("Envol");
    }
}
