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
    private bool playerFinded = false;
    private bool directionGauche;

    void Start()
    {
        // Mémorisation des points de patrouille
        Transform pathParent = transform.GetChild(0);
        pathParent.name = pathParent.parent.name + " path";

        int i = 0;
        foreach (Transform child in pathParent)
        {
            path.Add(child);
            child.name = $"{i}";
            i++;
        }

        pathParent.parent = transform.parent;

        // Initialisation des variables importantes
        player = GameObject.FindGameObjectWithTag("Player");
        currentTargetPos = path[currentTargetIndex].position;
    }

    void Update()
    {
        Vector2 currentPos = transform.position;

        // Déplacement
        if (!isWaiting && !playerFinded)
        {
            if (currentPos.x == currentTargetPos.x) // Pause entre chaque point
            {
                StartCoroutine(PausePolicier());
            }
            else // Déplacement vers un point
            {
                directionGauche = currentPos.x > currentTargetPos.x;

                float step = maxSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(currentPos, new Vector2(currentTargetPos.x, transform.position.y), step);
            }
        }

        // Détection du joueur
        playerFinded = false;
        float dist = Vector2.Distance(currentPos, player.transform.position);
        if (dist <= viewDistance) // Vérification de la distance (cercle de vision)
        {
            float angle = Vector2.Angle(transform.right, (player.transform.position - transform.position).normalized);

            if ((directionGauche && angle > 135) || (!directionGauche && angle < 45)) // Vérification de l'angle (cône de vision)
            {
                if (!(Physics2D.Raycast(transform.position, (player.transform.position - transform.position), dist, layerMask))) // Vérification de la présence d'obstacles
                {
                    playerFinded = true;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    Debug.Log("Je vois le joueur");
                }
            }
        }

        // Je change la couleur pour un feedback visuel. Mais ce n'est que pour les tests, cette fonction disparaitra
        if(!playerFinded)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    // Pause du policier
    IEnumerator PausePolicier()
    {
        isWaiting = true;
        currentTargetIndex = (currentTargetIndex + 1) % path.Count;
        currentTargetPos = path[currentTargetIndex].position;
        yield return new WaitForSeconds(TempsDePause);
        isWaiting = false;
    }

    // Collision entre deux policiers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ennemy")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        }
    }

    // Cette fonction ne sert que pour la visualisation du cône de vision dans l'inspecteur. Elle peut être supprimée sans conséquence sur le mode play
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