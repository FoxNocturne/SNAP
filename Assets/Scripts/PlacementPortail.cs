using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPortail : MonoBehaviour
{
    public bool tutoriel;
    public bool niveau1;

    [SerializeField] private Image occlusionPlacement;
    [SerializeField] private GameObject portailRadius;
    [SerializeField] private GameObject portailPrefab;
    [SerializeField] private Color[] portailColors = new Color[3];

    private GameObject portailPlacing, actualDimensionPortal, targetDimensionPortal;
    private Vector2 relativePositionFromPlayer;
    private Snap snapScript;
    private float moveHorizontal, moveVertical;
    private bool placing;
    private bool placingOkay;
    private bool targetIsNext;

    AudioSource sonPortailEnclencher;
    public AudioClip[] sonPortail;

    private void Start()
    {
        occlusionPlacement.enabled = false;
        portailRadius.SetActive(false);

        snapScript = GetComponent<Snap>();
        sonPortailEnclencher = GetComponent<AudioSource>();

        // Le joueur ignore les layers de transition au départ
        IgnoreAllTransition();
    }

    private void Update()
    {
        if (tutoriel)
            return;

        // Appui sur L2 ou R2
        if (Input.GetButtonDown("Portail"))
        {
            if (DestroyExistingPortal())
                return;

            Placement();
        }

        // Le portail est en train d'être placé
        if (placing)
        {
            DeplacementPortailPlacing();
            CheckPortailPlacingPossibility();
        }

        if (Input.GetButtonUp("Portail"))
        {
            Creer();
        }
    }

    // Vérifie si le bouton a été utilisé pour détruire un portail existant
    private bool DestroyExistingPortal()
    {
        //int actualDimension = snapScript.GetActualDimension();
        //int targetDimension = Input.GetAxis("Portail") == 1 ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension();

        if (actualDimensionPortal == null)
            return false;

        if ((actualDimensionPortal.layer == snapScript.GetActualDimension() + 9) ||
            (targetDimensionPortal.layer == snapScript.GetActualDimension() + 9))
        {
            Destroy(actualDimensionPortal);
            Destroy(targetDimensionPortal);

            return true;
        }

        return false;
    }

    // Lance le placement du portail
    private void Placement()
    {
        placing = true;
        occlusionPlacement.enabled = true;
        portailRadius.SetActive(true);

        portailPlacing = Instantiate(portailPrefab, transform);
        portailPlacing.transform.localPosition = new Vector2(3, 0);
        portailPlacing.GetComponent<Portail>().enabled = false;
        var colliders = portailPlacing.GetComponents<CircleCollider2D>();
        foreach (var collider in colliders)
            collider.enabled = false;
    }

    // Deplace le portail de placement avec le joystick de droite 
    private void DeplacementPortailPlacing()
    {
        float inverseXAxis = 1;

        if (transform.localScale.x < 0)
            inverseXAxis = -1;

        float horizontal = portailPlacing.transform.localPosition.x + Input.GetAxisRaw("Horizontal Portal") * Time.deltaTime * 5 * inverseXAxis;
        float vertical   = portailPlacing.transform.localPosition.y - Input.GetAxisRaw("Vertical Portal") * Time.deltaTime * 5;
        Vector2 newPosition = new Vector2(horizontal, vertical);

        if (Vector2.Distance(newPosition, Vector2.zero) < 5)
            portailPlacing.transform.localPosition = newPosition;
    }

    // Vérifie la possibilité de placer le portail à cet endroit
    private void CheckPortailPlacingPossibility()
    {
        placingOkay = true;
        int targetDimension = (Input.GetAxis("Portail") == 1 ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension()) + 9;

        // Test de la dimension actuelle
        if (Physics2D.OverlapCircle(portailPlacing.transform.position, 0.8f, LayerMask.GetMask(LayerMask.LayerToName(snapScript.GetActualDimension() + 9))))
            placingOkay = false;

        // Test de la dimension cible
        if (Physics2D.OverlapCircle(portailPlacing.transform.position, 0.8f, LayerMask.GetMask(LayerMask.LayerToName(targetDimension))))
            placingOkay = false;

        ChangeColor(portailPlacing, placingOkay ? Color.white : Color.red);
    }

    // Création du portail
    private void Creer()
    {
        if (portailPlacing == null)
            return;

        // Désactivation du placement
        portailRadius.SetActive(false);
        occlusionPlacement.enabled = false;
        placing = false;

        Vector2 portalPosition = portailPlacing.transform.position;
        Destroy(portailPlacing);

        if (!placingOkay)
            return;

        int actualDimension = snapScript.GetActualDimension();
        int targetDimension = Input.GetAxis("Portail") == 1 ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension();

        // Création du premier portail
        actualDimensionPortal = Instantiate(portailPrefab, portalPosition, transform.rotation);
        SetLayer(actualDimensionPortal, actualDimension + 9);
        ChangeColor(actualDimensionPortal, portailColors[targetDimension]);

        // Création du second portail
        targetDimensionPortal = Instantiate(portailPrefab, portalPosition, transform.rotation);
        SetLayer(targetDimensionPortal, targetDimension + 9);
        ChangeColor(targetDimensionPortal, portailColors[actualDimension]);

        // Lien entre les deux portails 
        actualDimensionPortal.GetComponent<Portail>().portailLinked = targetDimensionPortal.GetComponent<Portail>();
        targetDimensionPortal.GetComponent<Portail>().portailLinked = actualDimensionPortal.GetComponent<Portail>();
    }

    // Ignore les collisions avec les layers de transition
    private void IgnoreAllTransition()
    {
        Physics2D.IgnoreLayerCollision(8, 12, true);
        Physics2D.IgnoreLayerCollision(8, 13, true);
        Physics2D.IgnoreLayerCollision(8, 14, true);
    }

    // Change la couleur des particules du portail
    private void ChangeColor(GameObject portal, Color newColor)
    {
        #pragma warning disable CS0618 // Le type ou le membre est obsolète
        foreach (Transform child in portal.transform)
            child.GetComponent<ParticleSystem>().startColor = newColor;
    }

    private void SetLayer(GameObject portal, int newLayer)
    {
        portal.layer = newLayer;

        foreach (Transform child in portal.transform)
            child.gameObject.layer = newLayer;
    }
}
