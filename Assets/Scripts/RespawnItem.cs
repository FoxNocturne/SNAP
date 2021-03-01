using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    private Vector3 itemPosition;
    public LayerMask itemLayer;
    public bool isDead;


    private void Start()
    {
        isDead = false;

        if (transform.tag == "Item")
        {
            itemPosition =new Vector3( GetComponent<Transform>().position.x,GetComponent<Transform>().position.y, 0);
            itemLayer = gameObject.layer;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || isDead)
        {
            RespawnPositionItem();
        }
    }
    private void RespawnPositionItem()
    {

        transform.position = itemPosition;
        gameObject.layer = itemLayer;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DeadZones

        if (collision.tag == "Dead")
        {
            isDead = true;
            RespawnPositionItem();
        }
    }

}
