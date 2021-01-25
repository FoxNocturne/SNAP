using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static Vector3 reachedPoint = new Vector3();
    public GameObject CheckEffect;
    // Checkpoints
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("détecté");
            GameObject effectCheck = Instantiate(CheckEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
            reachedPoint = transform.position;
            Destroy(gameObject);            
        }
        
    }
}
