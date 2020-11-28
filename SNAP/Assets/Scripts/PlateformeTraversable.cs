using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeTraversable : MonoBehaviour
{
    BoxCollider2D player, thisCollider;

    private void Start()
    {
        player       = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float playerPosY = player.transform.position.y - (player.transform.localScale.y * player.size.y) / 2;
        Physics2D.IgnoreCollision(player, thisCollider, (playerPosY < transform.position.y));
    }
}
