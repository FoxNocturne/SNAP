using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    public bool activeControl = true;
    private bool directionGauche;
    public float dashSpeed;
    public float maxSpeed = 5;
    private float speed;
    private float tailleX;
    private float tailleY;
    public float jump = 100;
    AudioSource SonHero;
    Rigidbody2D rb;
    public Transform circleGround;
    public GameObject phantomEffect;
    public GameObject ObserveThisThing;
    public LayerMask whatIsGround;
    public bool onTheGround = false;
    bool ghost = false;
    bool dash = false;
    bool canDash = false;
    bool canClimb = false;
    bool isPulling = false;
    Transform objectPulling;

    void Start()
    {
        tailleX = GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2;
        tailleY = GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2;
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
        onTheGround = Physics2D.OverlapArea(new Vector2(transform.position.x - tailleX, transform.position.y - tailleY), new Vector2(transform.position.x + tailleX, transform.position.y - tailleY - 0.1f), whatIsGround);

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
                if (objectPulling)
                {
                    objectPulling.Translate(Vector2.right * moveHorizontal * maxSpeed * Time.deltaTime);
                }
            }

            // SAUTER 
            if (Input.GetButtonDown("Sauter") && onTheGround && !isPulling)
            {
                rb.gravityScale = 2; // Initialise la gravité
                canClimb = false; // Cancel l'escalade
                rb.AddForce(transform.up * jump);
            }

            // DASH
            if (Input.GetButtonDown("Dash") && !dash && canDash && !isPulling)
            {
                if (onTheGround)
                    StartCoroutine(DashSol());
                else
                    StartCoroutine(Dash());
            }

            // ATTRAPER
            if (Input.GetButtonDown("Attraper"))
            {
                float distance = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
                float sizeQuarter = transform.position.y - GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 4;

                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, sizeQuarter), directionGauche ? Vector2.left : Vector2.right, distance, whatIsGround);
                if (hit && hit.transform.tag == "Item")
                {
                    isPulling = true;

                    objectPulling = hit.transform;
                    GetComponent<SpriteRenderer>().color = Color.gray;
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }

            if (Input.GetButtonUp("Attraper") || !onTheGround || (objectPulling && objectPulling.GetComponent<Rigidbody2D>().velocity.y < -1))
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                if (objectPulling)
                    objectPulling.GetComponent<SpriteRenderer>().color = Color.blue;

                objectPulling = null;
                isPulling = false;
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
        effect.transform.localScale = transform.localScale;
        effect.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(timeSpawn);
        ghost = false;
    }

    // TEMPS DE DASH
    IEnumerator Dash()
    {
        dash = true;
        canDash = false;

        maxSpeed *= 3; // accélération
        moveHorizontal = 0;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        yield return new WaitForSeconds(0.2f);

        maxSpeed = speed;
        rb.gravityScale = 2;
        rb.velocity = new Vector2(0, 0);
        
        dash = false;
    }

    // TEMPS DE DASH AU SOL
    IEnumerator DashSol()
    {
        dash = true;
        canDash = false;

        maxSpeed *= 3; // accélération
        moveHorizontal = 0;
        transform.Rotate(0, 0, directionGauche ? -90 : 90);
        transform.position = new Vector2(transform.position.x, transform.position.y + tailleX - tailleY);
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        yield return new WaitForSeconds(0.2f);

        // Le dash au sol continue tant qu'il y a quelque chose au-dessus du personnage
        while(Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + tailleY), whatIsGround))
        {
            yield return new WaitForSeconds(0.05f);
        }

        maxSpeed = speed;
        transform.Rotate(0, 0, directionGauche ? 90 : -90);
        transform.position = new Vector2(transform.position.x, transform.position.y - tailleX + tailleY);
        rb.gravityScale = 2;
        rb.velocity = new Vector2(0, 0);

        dash = false;
    }

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

    public bool isMovingLeft()
    {
        return directionGauche;
    }
}

