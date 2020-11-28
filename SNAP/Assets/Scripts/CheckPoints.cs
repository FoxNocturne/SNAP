using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static Vector3 reachedPoint = new Vector3();

    // Checkpoints
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            reachedPoint = transform.position;
            
        }
        
    }
}
