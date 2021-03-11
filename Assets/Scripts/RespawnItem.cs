using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    //public GameObject item;
    private Vector3 itemPosition;
    public LayerMask itemLayer;
    public SpriteRenderer itemSpriteO;
    public int itemSprite;

    public bool isDead;
    Rigidbody2D rbCaisse;


    private void Start()
    {

        rbCaisse = GetComponent<Rigidbody2D>();
        isDead = false;

        //item = GameObject.FindGameObjectWithTag("Item");
        itemPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        itemLayer = gameObject.layer;
        itemSprite = GetComponent<SpriteRenderer>().sortingLayerID;



    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) || isDead)

        {
            RespawnPositionItem();
        }

    }
    private void RespawnPositionItem()
    {

        gameObject.transform.position = itemPosition;
        gameObject.layer = itemLayer;
        isDead = false;
        rbCaisse.velocity = new Vector2(0, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = itemSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DeadZones
        if (collision.tag == "Dead")
        {
            isDead = true;

            RespawnPositionItem();
        }
        else
        {
            isDead = false;
        }
    }

}
