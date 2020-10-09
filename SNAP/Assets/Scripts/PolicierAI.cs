using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicierAI : MonoBehaviour
{
    public float maxSpeed = 5;
    public float TempsDePause = 2;

    private List<Transform> path = new List<Transform>();
    private int currentTarget = 0;

    Rigidbody2D rb;
    public Transform circleGround;
    public LayerMask whatIsGround;

    void Start()
    {
        foreach(Transform child in transform.GetChild(0))
        {
            path.Add(child);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = new Vector2((transform.position.x - path[currentTarget].position.x) * maxSpeed * Time.deltaTime, 0);
        rb.velocity = -velocity;
    }
}
