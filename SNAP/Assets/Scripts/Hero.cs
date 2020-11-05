using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public bool activeControl = true;
    public float jump = 100;
    public float dashSpeed;
    public float dashDistance;
    public float maxSpeed = 5;
    float speed;
    float moveHorizontal;
    float moveVertical; AudioSource SonHero;
    Rigidbody2D rb;
    public Transform circleGround;
    public GameObject phantomEffect;
    public GameObject ObserveThisThing;
    public LayerMask whatIsGround;
    public bool onTheGround = false;
    bool directionGauche;
    bool ghost = false;
    bool dash = false;
    bool canDash = false;
    bool canClimb = false;

    void Start()
    {
        speed = maxSpeed;
        SonHero = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (activeControl && !dash)
        {
            // Valeur du mouvement horizontal (1 = droite / -1 = gauche)
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        // DEPLACEMENT DU PERSONNAGE

    }

    void Update()
    {
        whatIsGround = Physics2D.GetLayerCollisionMask(8);
        onTheGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 1.61f), 0.1f, whatIsGround);

        if (!canDash && onTheGround)
            canDash = true;

        // DEPLACEMENT
        if (activeControl && !dash)
        {
            if (moveHorizontal != 0)
                directionGauche = moveHorizontal < 0;

            // En escalade
            if (canClimb)
            {
                transform.Translate(new Vector2(moveHorizontal * maxSpeed * Time.deltaTime, moveVertical * maxSpeed * Time.deltaTime));
            }
            // Au sol
            else
            {
                transform.Translate(Vector2.right * moveHorizontal * maxSpeed * Time.deltaTime);
            }

            // SAUTER 
            if (Input.GetKeyDown(KeyCode.Space) && onTheGround)
            {
                rb.gravityScale = 2; // Initialise la gravité
                canClimb = false; // Cancel l'escalade
                rb.AddForce(transform.up * jump);
            }

            // DASH
            if (Input.GetKeyDown(KeyCode.E) && !dash && canDash)
            {
                if (onTheGround)
                    StartCoroutine(DashSol());
                else
                    StartCoroutine(DashAir());
            }


        }
        
        if (dash && !ghost)
        {
            StartCoroutine(GhostEffect(0.02f));
        }
        // Sortir d'un panneau ou d'une affiche lorsqu'on le lit
        if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("ObserveThisThing") != null)
        {
            Destroy(GameObject.Find("ObserveThisThing"));
            Time.timeScale = 1;
            activeControl = true;
        }
    }

    // EFFET DE GHOST
    IEnumerator GhostEffect(float timeSpawn)
    {
        ghost = true;
        GameObject effect = Instantiate(phantomEffect, transform.position, Quaternion.identity) as GameObject;
        effect.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(timeSpawn);
        ghost = false;
    }

    IEnumerator DashAir()
    {
        dash = true;
        canDash = false;
        float initialPosX = transform.position.x;
        float tailleX = GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2;
        float tailleY = GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2;

        moveHorizontal = 0;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        while(!Physics2D.OverlapArea(new Vector2(transform.position.x + (directionGauche ? - tailleX : tailleX), transform.position.y + tailleY), 
                                     new Vector2(transform.position.x + (directionGauche ? - tailleX - 0.01f : tailleX + 0.01f), transform.position.y - tailleY), 
                                     whatIsGround))
        {
            if (directionGauche ? (transform.position.x <= initialPosX - dashDistance)
                                : (transform.position.x >= initialPosX + dashDistance))
                break;
            yield return new WaitForSeconds(0.001f);
        }

        rb.gravityScale = 2;
        rb.velocity = Vector2.zero;

        dash = false;
    }

    IEnumerator DashSol()
    {
        dash = true;
        canDash = false;
        float oldSizeX = transform.localScale.x;
        float oldSizeY = transform.localScale.y;
        transform.localScale = new Vector2(oldSizeY, oldSizeX);
        float initialPosX = transform.position.x;
        float tailleX = GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2;
        float tailleY = GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2;

        transform.position = new Vector2(transform.position.x, transform.position.y - tailleX + tailleY);
        moveHorizontal = 0;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        while (!Physics2D.OverlapArea(new Vector2(transform.position.x + (directionGauche ? -tailleX : tailleX), transform.position.y + tailleY),
                                          new Vector2(transform.position.x + (directionGauche ? -tailleX - 0.01f : tailleX + 0.01f), transform.position.y - tailleY + 0.05f),
                                          whatIsGround))
        {
            if (directionGauche ? (transform.position.x <= initialPosX - dashDistance)
                                : (transform.position.x >= initialPosX + dashDistance))
                break;
            yield return new WaitForSeconds(0.001f);
        }

        while (Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + oldSizeY - tailleY), whatIsGround))
            yield return new WaitForSeconds(0.05f);


        transform.localScale = new Vector2(oldSizeX, oldSizeY);
        transform.position = new Vector2(transform.position.x, transform.position.y + (GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2 - tailleY));

        rb.gravityScale = 2;
        rb.velocity = Vector2.zero;

        dash = false;
    }// 
     /**/
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si Mr.X est devant un mur à escalader ou une échelle
        if (collision.tag == "Climb" && moveVertical > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            canClimb = true;
        }

        // Si Mr.X est devant un objet à ramasser
        if (collision.tag == "Item")
        {
            // RAMASSER UN OBJET
            if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.tag == "Display")
        {
            // RAMASSER UN OBJET
            if (Input.GetKeyDown(KeyCode.R) && onTheGround)
            {
                if (GameObject.Find("ObserveThisThing") == null)
                {
                    GameObject DisplayObject = Instantiate(ObserveThisThing, transform.position, Quaternion.identity);
                    DisplayObject.name = "ObserveThisThing";
                    DisplayObject.transform.GetChild(0).GetComponent<Image>().sprite = collision.GetComponent<ObserveThisThing>().Object_Picture;
                    DisplayObject.transform.GetChild(1).GetComponent<Text>().text = collision.GetComponent<ObserveThisThing>().text;
                    Time.timeScale = 0;
                    activeControl = false;
                }
            }

        }
    }
    // Sort de la zone d'escalade
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climb")
        {
            rb.gravityScale = 2;
            canClimb = false;
        }
    }
    // Détecte si Mr.X touche le sol 
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(circleGround.transform.position, 0.1f);
    }
}
