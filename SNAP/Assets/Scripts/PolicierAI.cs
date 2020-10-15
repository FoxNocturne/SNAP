using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicierAI : MonoBehaviour
{
    public float maxSpeed = 5;
    public float TempsDePause = 2;
    public GameObject player;
    public float viewDistance;
    public LayerMask layerMask;

    private List<Transform> path = new List<Transform>();
    private int currentTargetIndex = 0;
    private Vector2 currentTargetPos;
    private bool isWaiting = false;
    private bool directionGauche;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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
                directionGauche = currentPos.x > currentTargetPos.x;

                float step = maxSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(currentPos, new Vector2(currentTargetPos.x, transform.position.y), step);
            }
        }

        if (Vector2.Distance(currentPos, player.transform.position) <= viewDistance)
        {
            float angle = Vector2.Angle(transform.right, (player.transform.position - transform.position).normalized);

            if(directionGauche)
            {
                if(angle > 135)
                {
                    Debug.Log("Le joueur est dans mon cône de vision");

                    if (Physics2D.Raycast(transform.position, Vector2.left, viewDistance, layerMask))
                    {
                        Debug.Log("Je le vois !");
                    }
                }
            }else
            {
                if(angle < 45)
                {
                    Debug.Log("Le joueur est dans mon cône de vision");
                }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ennemy")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        float angleX = Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) * (directionGauche ? -1 : 1);
        float angleY = Mathf.Cos(angleInDegrees * Mathf.Deg2Rad) * (directionGauche ? -1 : 1);
        return new Vector3(angleX, angleY, 0);
    }
}