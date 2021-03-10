using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasLvl2 : MonoBehaviour
{
    Transform player;

    [Header("Checkpoints")]
    public GameObject lvl2;
    public GameObject lvl3;

    private int actualLevel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        actualLevel = 1;
    }

    void Update()
    {
        refreshCollider();

        switch (actualLevel)
        {
            case 1:
                Level1();
                break;

            case 2:
                Level2();
                break;

            case 3:
                Level3();
                break;
        }
    }

    void Level1()
    {
        Vector3 pos = player.position;

        if (player.position.y < 14.2)
            pos.x = Mathf.Clamp(pos.x, 4.54f, 58.04f);
        else if (player.position.x > 69.5f)
            actualLevel++;  

        pos.y = Mathf.Clamp(pos.y, 5.92f, 16);
        pos.z = -10;
        
        transform.position = pos;
    }

    void Level2()
    {
        if (player.position.x < 88)
        {
            Vector3 pos = player.position;
            pos.y = Mathf.Clamp(pos.y, -0.3f, 16);
            pos.z = -10;
            transform.position = pos;

            foreach (Transform child in transform)
                child.GetComponent<Camera>().orthographicSize += (8 - child.GetComponent<Camera>().orthographicSize);

        }
        else if (player.position.x < 175)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x < 131.8 ? 109.9f : 152.4f, 4.17f, -10), 15 * Time.deltaTime);
            foreach (Transform child in transform)
                child.GetComponent<Camera>().orthographicSize += (12.5f - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
        }
        else
            actualLevel++;
    }

    void Level3()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(205.5f, 8.3f, -10), 15 * Time.deltaTime);
        foreach (Transform child in transform)
            child.GetComponent<Camera>().orthographicSize += (20 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
    }

    void refreshCollider()
    {
        var actualSize = transform.GetChild(0).GetComponent<Camera>().orthographicSize;
        GetComponent<BoxCollider2D>().size = new Vector2(actualSize * 3.5625f, actualSize * 2);
    }
}
