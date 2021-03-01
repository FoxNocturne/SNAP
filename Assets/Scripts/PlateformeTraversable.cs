using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeTraversable : MonoBehaviour
{
    Snap player;
    BoxCollider2D thisCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Snap>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        thisCollider.enabled = (player.GetActualDimension() + 9 == gameObject.layer);
    }
}
