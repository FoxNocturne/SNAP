using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{

    public float lookRadius = 10f;
    //public Transform target;
    public GameObject player;
   // public NavMeshAgent agent;
    



    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;
        
        float distance = Vector3.Distance(currentPos, player.transform.position);

        if (distance<=lookRadius)
        {
            Debug.Log("MORTTTTTTT!!!!!!!!!!!!!!!");
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
