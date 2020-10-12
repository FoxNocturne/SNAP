using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicierAI : MonoBehaviour
{
    public float maxSpeed = 5;
    public float TempsDePause = 2;

    private List<Transform> path = new List<Transform>();
    private int currentTargetIndex = 0;
    private Vector2 currentTargetPos;
    private bool isWaiting = false;

    void Start()
    {
        Transform pathParent = transform.GetChild(0);
        foreach (Transform child in pathParent)
        {
            path.Add(child);
        }

        pathParent.parent = transform.parent;

        currentTargetPos = path[currentTargetIndex].position;
    }

    void Update()
    {
        Vector2 currentPos = transform.position;

        if (!isWaiting)
        {
            if (currentPos.x == currentTargetPos.x)
            {
                StartCoroutine(PausePolicier());
            }
            else
            {
                float step = maxSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(currentPos, new Vector2(currentTargetPos.x, transform.position.y), step);
            }
        }
    }

    IEnumerator PausePolicier()
    {
        isWaiting = true;
        currentTargetIndex = (currentTargetIndex + 1) % path.Count;
        currentTargetPos = path[currentTargetIndex].position;
        yield return new WaitForSeconds(TempsDePause);
        isWaiting = false;
    }
}
