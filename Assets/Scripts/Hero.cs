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
    private bool flipLeft = false;
    public float dashSpeed;
    public float maxSpeed = 5;
    private float speed;
    private float tailleX;
    private float tailleY;
    public float jump = 100;
    Animator anim;    // ON AJOUTE ANIMATOR POUR GERER L'ANIMATION DE MR.X
    AudioSource SonHero;
    public AudioClip[] sonMrX;
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
    RaycastHit2D hit;

    BoxCollider2D hitbox;
    float TimeNoMove = 0;


    void Start()
    {
        tailleX = GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2;
        tailleY = GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2;
        speed = maxSpeed;
        SonHero = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();// ON AJOUTE LA REFERENCE D'ANIMATOR POUR GERER L'ANIMATION DE MR.X
    }

    void FixedUpdate()
    {
        // Debug.Log(onTheGround);
        if (activeControl && !dash)
        {
            // Valeur du mouvement horizontal (1 = droite / -1 = gauche)
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            onTheGround = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 1.65f), new Vector3(0.45f,0.1f, 1f), 0, whatIsGround);
            
            if(onTheGround)
            {
                anim.SetBool("jump", false); // QUAND TOUCHE LE SOL, DESACTIVE L'ANIMATION DE SAUT POUR L'ATTERRISAGE
                anim.SetBool("onTheGround", true);
            }
            else
            {
                anim.SetBool("onTheGround", false);
            }
        }
        // DEPLACEMENT DU PERSONNAGE

    }


    void Update()
    {
        whatIsGround = Physics2D.GetLayerCollisionMask(8);

        // onTheGround = Physics2D.OverlapArea(new Vector2(transform.position.x - tailleX, transform.position.y - tailleY), new Vector2(transform.position.x + tailleX, transform.position.y - tailleY - 0.1f), whatIsGround);
        anim.SetBool("Grab", isPulling );
        anim.SetBool("directionGauche", flipLeft);
        anim.SetBool("climb", canClimb);
        anim.SetFloat("pousser", moveHorizontal);
        anim.SetFloat("moveVertical", rb.velocity.y); // IL REGARDE SI MR.X VA VERS LE HAUT OU VERS LE BAS POUR L'ANIMATION.
                                                        // S'IL VA VERS LE BAS (moveVertical < 0), ON LANCE LA TRANSITION DE LA CHUTE

        if (!canDash && onTheGround)
        {
            canDash = true;

        }
            


        // DEPLACEMENT
        if (activeControl && !dash)
        {
            //SonHero.playOnAwake(sonMrX[6], 1f);
            if (moveHorizontal != 0)
            {
                anim.SetBool("run", true); // IL COURT, ACTIVE L'ANIMATION DE COURSE
                directionGauche = moveHorizontal < 0;
                if(flipLeft != directionGauche && !isPulling)
                {
                    LookLeft();
                }
            }
            else
            {
                anim.SetBool("run", false); // IL S'ARRETE, DESACTIVE L'ANIMATION DE COURSE
            }


            // En escalade
            if (canClimb)
            {
                transform.Translate(new Vector2(moveHorizontal * maxSpeed * Time.deltaTime, moveVertical * maxSpeed * Time.deltaTime));
                if((moveHorizontal < moveVertical && moveVertical > 0) || (moveHorizontal > moveVertical && moveVertical < 0) )
                {
                    if(moveVertical < 0)
                    {
                        anim.speed = moveVertical * -1;
                    }
                    else
                    {
                        anim.speed = moveVertical;
                    }
                }
                else
                {
                    if(moveHorizontal < 0)
                    {
                        anim.speed = moveHorizontal * -1;
                    }
                    else
                    {
                        anim.speed = moveHorizontal;
                    }                   
                }
                
                
            }
            // Au sol
            else
            {
                anim.speed = 1;
                transform.Translate(Vector2.right * moveHorizontal * maxSpeed * Time.deltaTime);
                if (objectPulling)
                {
                    objectPulling.Translate(Vector2.right * moveHorizontal * maxSpeed * Time.deltaTime);
                }
            }

            // SAUTER 
            if (Input.GetButtonDown("Sauter") && onTheGround && !isPulling)
            {
                anim.SetBool("jump", true); // QUAND TOUCHE LE SOL, DESACTIVE L'ANIMATION DE SAUT POUR L'ATTERRISAGE
                rb.gravityScale = 2; // Initialise la gravité
                canClimb = false; // Cancel l'escalade
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(transform.up * jump);
            }

            // DASH
            if (Input.GetButtonDown("Dash") && !dash && canDash && !isPulling)
            {
                
                if (onTheGround) {
                    StartCoroutine(DashSol());

                }

                else
                {
                    StartCoroutine(Dash());
                }
                    
            }

            // Checkpoint TEST
            if (Input.GetKeyDown(KeyCode.T))
            {
              
                    RestartLevel();
            }

            // ATTRAPER
            float distance = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
            float sizeQuarter = transform.position.y - GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 4;
            hit = Physics2D.Raycast(new Vector2(transform.position.x, sizeQuarter), Vector2.right, distance, whatIsGround);
            

            if (Input.GetButtonDown("Attraper"))
            {
                Debug.Log(hit.transform);
                if (hit && hit.transform.tag == "Item")
                {
                    isPulling = true;

                    objectPulling = hit.transform;
                    // GetComponent<SpriteRenderer>().color = Color.gray;
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.gray;
                    hit.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }

            if(objectPulling != null)
            {
                if (Input.GetButtonUp("Attraper") || (isPulling && !onTheGround) || (objectPulling && objectPulling.GetComponent<Rigidbody2D>().velocity.y < -1))
                {
                    // GetComponent<SpriteRenderer>().color = Color.red;
                    hit.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;               
                    if (objectPulling)
                        objectPulling.GetComponent<SpriteRenderer>().color = Color.blue;

                    objectPulling = null;
                    isPulling = false;  
                }
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

    //Recharger au Checkpoint
    private void RestartLevel()
    {
       
        transform.position = CheckPoints.reachedPoint;
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
        anim.SetBool("Dash", true);
        dash = true;
        canDash = false;

        maxSpeed *= 3; // accélération
        moveHorizontal = 0;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Dash", false);
        maxSpeed = speed;
        rb.gravityScale = 2;
        rb.velocity = new Vector2(0, 0);
        
        dash = false;
    }

    // TEMPS DE DASH AU SOL
    IEnumerator DashSol()
    {
        TimeNoMove = 0;
        dash = true;
        canDash = false;
        anim.SetBool("Dash", true);
        maxSpeed *= 3; // accélération
        moveHorizontal = 0;
        hitbox.offset = new Vector2(0, -1.338f);
        hitbox.size = new Vector2(1, 1.363f);
        // transform.Rotate(0, 0, directionGauche ? -90 : 90);
        //transform.position = new Vector2(transform.position.x, transform.position.y + tailleX - tailleY);
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * (directionGauche ? -1 : 1), 0);

        yield return new WaitForSeconds(0.2f);

        // Le dash au sol continue tant qu'il y a quelque chose au-dessus du personnage
        while(Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + (tailleY - 1)), whatIsGround))
        {
            yield return new WaitForSeconds(0.05f);

            if((rb.velocity.x < 20 && !directionGauche) || (rb.velocity.x > -20 && directionGauche) )
            {            
                
                TimeNoMove += Time.deltaTime;
                 if(TimeNoMove > 0.02f)
                {
                    break;
                }
            }
        }
        anim.SetBool("Dash", false);
        maxSpeed = speed;
        hitbox.offset = new Vector2(0, -0.62f);
        hitbox.size = new Vector2(1, 2.8f);
        //transform.Rotate(0, 0, directionGauche ? 90 : -90);
        //transform.position = new Vector2(transform.position.x, transform.position.y - tailleX + tailleY);
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

    // Entrer de collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DeadZones

        if (collision.tag == "Dead")
        {
            RestartLevel();
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

    void LookLeft()
    {
        flipLeft = !flipLeft;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.yellow;
        // Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 2.5f), new Vector3(0.1f,0.1f, 0.1f));
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 1.65f), new Vector3(0.45f,0.1f, 1f));
    } 

   /* void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        float size = transform.position.y - GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 4;
        float distance2 = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
        Gizmos.DrawRay(new Vector2(transform.position.x, size), flipLeft ? Vector2.left : Vector2.right);

        
    } */

    public void pasMrX()
    {
        // Tu mets ton son ici
    }    
}

