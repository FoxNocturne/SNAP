using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    private Vector3 itemPosition;


    private void Start()
    {
        if (transform.tag == "Item")
        {
            itemPosition =new Vector3( GetComponent<Transform>().position.x,GetComponent<Transform>().position.y, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RespawnPositionItem();
        }
    }
    private void RespawnPositionItem()
    {

        transform.position = itemPosition;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DeadZones

        if (collision.tag == "Dead")
        {
            RespawnPositionItem();
        }
    }

}
