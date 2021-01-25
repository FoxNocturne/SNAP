using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasLvl2 : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 pos = player.position;
        pos.z = -10;
        transform.position = pos;
    }
}
