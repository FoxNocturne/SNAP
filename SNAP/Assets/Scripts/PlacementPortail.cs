using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPortail : MonoBehaviour
{
    public LayerMask testLayer;

    [SerializeField] private Image occlusionPlacement;
    [SerializeField] private GameObject portailRadius;
    [SerializeField] private GameObject portailPrefab;
    [SerializeField] private Color[] portailColors = new Color[3];

    private GameObject portailPlacing, portailActualDimension, portailTargetDimension;
    private Vector2 relativePositionFromPlayer;
    private Snap snapScript;
    private float moveHorizontal, moveVertical;
    private bool placing;
    private bool placingOkay;
    private bool targetIsNext;

    private void Start()
    {
        occlusionPlacement.enabled = false;
        portailRadius.SetActive(false);

        snapScript = GetComponent<Snap>();
    }

    private void FixedUpdate()
    {
        // Calcule la position du prochain portail
        if(portailPlacing)
        {
            moveHorizontal = Input.GetAxis("Horizontal Portal");
            moveVertical = -Input.GetAxis("Vertical Portal");

            relativePositionFromPlayer.x += moveHorizontal * 5 * Time.deltaTime;
            relativePositionFromPlayer.y += moveVertical * 5 * Time.deltaTime;
        }
    }

    private void Update()
    {
        // Déplace le portail 
        if (placing)
        {
            Vector2 nextPos = new Vector2(transform.position.x + relativePositionFromPlayer.x, transform.position.y + relativePositionFromPlayer.y);
            if (Vector2.Distance(transform.position, nextPos) <= 5) // On vérifie que le portail n'est pas hors de portée
                portailPlacing.transform.position = nextPos;
            else // On remet à jour l'offset si non
                relativePositionFromPlayer = portailPlacing.transform.position - transform.position;

            bool test = (Physics2D.OverlapCircle(portailPlacing.transform.position, portailPlacing.transform.localScale.x, LayerMask.GetMask(LayerMask.LayerToName(snapScript.GetActualDimension() + 9))) ||
                         Physics2D.OverlapCircle(portailPlacing.transform.position, portailPlacing.transform.localScale.x, LayerMask.GetMask(LayerMask.LayerToName((targetIsNext ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension()) + 9))));
            portailPlacing.GetComponent<SpriteRenderer>().color = test ? Color.red : Color.white;
            placingOkay = !test;
        }

        // Lance le placement ou détruit le portail s'il est déjà présent
        if (Input.GetButtonDown("Portail"))
        {
            targetIsNext = Input.GetAxis("Portail") == 1;

            // Je m'excuse d'avance pour le contenu de ce if
            // Il sert à fermer le portail en appuyant sur le bouton associé
            // Encore désolé...
            if ((portailActualDimension && snapScript.GetActualDimension() + 9 == portailActualDimension.layer && portailActualDimension.GetComponent<Portail>().targetDimension == (targetIsNext ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension())) ||
                (portailTargetDimension && snapScript.GetActualDimension() + 9 == portailTargetDimension.layer && portailTargetDimension.GetComponent<Portail>().targetDimension == (targetIsNext ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension())))
            {
                Destroy(portailActualDimension);
                Destroy(portailTargetDimension);
            }else
            {
                placing = true;
                Placer();
            }
        }

        // Place le portail une fois la touche relachée
        if (Input.GetButtonUp("Portail") && placing)
        {
            occlusionPlacement.enabled = false;
            portailRadius.SetActive(false);

            if (placingOkay)
            {
                if (portailActualDimension)
                {
                    Destroy(portailActualDimension);
                    Destroy(portailTargetDimension);
                }
                Creer();
            }else
            {
                placing = false;
                Destroy(portailPlacing);
            }

            placing = false;
        }
    }

    private void Placer()
    {
        occlusionPlacement.enabled = true;
        portailRadius.SetActive(true);
        int placementX = GetComponent<Hero>().isMovingLeft() ? -3 : 3;

        portailPlacing = Instantiate(portailPrefab, new Vector2(transform.position.x + placementX, transform.position.y), transform.rotation, transform);
        portailPlacing.layer = 8;

        // Ajout temporaire
        portailPlacing.GetComponent<CircleCollider2D>().enabled = false;
        //

        relativePositionFromPlayer = new Vector2(placementX, 0);
    }

    private void Creer()
    {
        portailActualDimension = portailPlacing;
        portailPlacing = null;

        int targetDimension = targetIsNext ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension();
        int actualDimension = snapScript.GetActualDimension();

        portailActualDimension.transform.parent = transform.parent;
        portailActualDimension.layer = actualDimension + 9;
        portailActualDimension.GetComponent<SpriteRenderer>().color = portailColors[targetDimension];
        portailActualDimension.GetComponent<Portail>().targetDimension = targetDimension;

        portailTargetDimension = Instantiate(portailPrefab, portailActualDimension.transform.position, transform.rotation, portailActualDimension.transform.parent);
        portailTargetDimension.layer = targetDimension + 9;
        portailTargetDimension.GetComponent<SpriteRenderer>().color = portailColors[actualDimension];
        portailTargetDimension.GetComponent<Portail>().targetDimension = actualDimension;

        // Ajout temporaire
        portailPlacing.GetComponent<CircleCollider2D>().enabled = true;
        portailActualDimension.GetComponent<Portail>().portalLinked = portailTargetDimension;
        portailTargetDimension.GetComponent<Portail>().portalLinked = portailActualDimension;
    }
}
