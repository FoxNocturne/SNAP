using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x < 27)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, -10);
        }else if (player.position.x < 44.50)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(35.6f, transform.position.y, -10), 10 * Time.deltaTime);
        }else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(53.4f, transform.position.y, -10), 15 * Time.deltaTime);
        }
    }
}
