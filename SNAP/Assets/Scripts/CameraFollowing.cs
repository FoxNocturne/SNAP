using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x < 30)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, -10);
        }else if (player.position.x < 49)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(40, 3.5f, -10), 10 * Time.deltaTime);
        }else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(59.5f, 4.5f, -10), 15 * Time.deltaTime);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (6 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
    }
}
