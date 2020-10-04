using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    public bool activeControl = true;

    public float maxSpeed = 5;
    private float speed;
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


    void Start()
    {
        speed = maxSpeed;
        SonHero = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(activeControl)
        {
            // Valeur du mouvement horizontal (1 = droite / -1 = gauche)
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else if(!activeControl && moveHorizontal != 0)
        {
            moveHorizontal = 0;
        }
        // DEPLACEMENT DU PERSONNAGE
        
    }

    void Update()
    {
        onTheGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.1f, whatIsGround);
        // SAUTER
        if(activeControl)
        {
            if(canClimb)
            {
                transform.Translate(new Vector2(moveHorizontal * maxSpeed * Time.deltaTime, moveVertical * maxSpeed * Time.deltaTime));
            }
            else
            {
                transform.Translate(Vector2.right * moveHorizontal * maxSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space) && onTheGround)
            {
                rb.gravityScale = 2;
                canClimb = false;
                rb.AddForce(transform.up * jump);
            }

            // DASH
            if (Input.GetKeyDown(KeyCode.E) && !dash)
            {
                StartCoroutine(Dash());
            }


        }

        if(onTheGround && maxSpeed == speed && dash)
        {
            dash = false;
        }
        if(dash && !ghost)
        {
            StartCoroutine(GhostEffect(0.02f));
        }

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

    // TEMPS DE DASH
    IEnumerator Dash()
    {
        dash = true;
        maxSpeed *= 3;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 2;
        maxSpeed = speed;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(GameObject.Find("ObserveThisThing") == null)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climb")
        {
            rb.gravityScale = 2;
            canClimb = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(circleGround.transform.position, 0.1f);
    }
}
