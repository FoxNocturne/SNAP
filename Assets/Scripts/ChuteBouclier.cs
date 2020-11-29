using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteBouclier : MonoBehaviour
{
    public GameObject cameras;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && collision.gameObject.GetComponent<Hero>().onTheGround)
            cameras.GetComponent<CameraFollowing>().EvenementChuteBouclier(transform);
    }
}
