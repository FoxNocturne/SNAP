using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public float attractionForce = 1;

    [HideInInspector] public int targetDimension;
    [HideInInspector] public Portail portailLinked;

    Transform draggedObject;
    Vector2 defaultScale;
    float maxDistance, colliderMaxDistance;
    bool absorption = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.tag == "Item"))
            return;

        draggedObject = collision.transform;
        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        defaultScale = draggedObject.localScale;
        maxDistance = Mathf.Abs(Vector2.Distance(draggedObject.position, transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        absorption = true;
        colliderMaxDistance = Mathf.Abs(Vector2.Distance(draggedObject.position, transform.position));
    }

    private void Update()
    {
        if (draggedObject == null)
            return;

        draggedObject.tag = "Untagged";

        float distance = Mathf.Abs(Vector2.Distance(draggedObject.position, transform.position));
        float speed = Mathf.Clamp((3.5f - distance), 1.5f, float.MaxValue) * attractionForce * Time.deltaTime;

        draggedObject.position = Vector2.MoveTowards(draggedObject.position, transform.position, speed);
        draggedObject.localScale = defaultScale * Mathf.InverseLerp(0, maxDistance, distance);

        if (absorption)
            transform.localScale *= Mathf.InverseLerp(0, colliderMaxDistance, distance);
    }

    void OnDestroy()
    {
        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        draggedObject.localScale = defaultScale;
    }







    /* 
    private List<ObjetTransfert> objectsReceived = new List<ObjetTransfert>(); 
    private GameObject[] cameras = new GameObject[3]; 
 
    AudioSource sonPortailAspire; 
    public AudioClip[] sonPortailObjet; 
 
    private void Start() 
    { 
        cameras = GameObject.FindGameObjectsWithTag("MainCamera"); 
        sonPortailAspire = GetComponent<AudioSource>(); 
    } 
 
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (objectsReceived.Find(objet => objet.collider == collision) == null) 
        { 
            portailLinked.transferObject(new ObjetTransfert(collision, FindProvenance(collision.transform))); 
            sonPortailAspire.PlayOneShot(sonPortailObjet[1], 0.1f); 
 
            if (collision.tag == "Item") 
            { 
                collision.gameObject.layer = targetDimension + 12; 
                cameras[portailLinked.targetDimension].GetComponent<Camera>().cullingMask |= 1 << targetDimension + 12; // Ajoute le layer de transition au culling mask de la dimension de départ 
            } 
            else if (collision.tag == "Player") 
            { 
                collision.GetComponent<Snap>().ActiveSnap(targetDimension == collision.GetComponent<Snap>().GetNextDimension() ? 1 : -1); 
                sonPortailAspire.PlayOneShot(sonPortailObjet[1], 0.1f); 
            } 
        } 
    } 
 
    private void OnTriggerExit2D(Collider2D collision) 
    { 
        var objetTransfert = objectsReceived.Find(objet => objet.collider == collision); 
        if (objetTransfert != null) 
        { 
            if (collision.tag == "Item") 
            { 
                if (FindProvenance(collision.transform) == objetTransfert.provenance) 
                    collision.gameObject.layer = targetDimension + 9; 
                else 
                    collision.gameObject.layer = portailLinked.targetDimension + 9; 
            } 
            objectsReceived.Remove(objetTransfert); 
            cameras[targetDimension].GetComponent<Camera>().cullingMask &= ~(1 << portailLinked.targetDimension + 12); // On retire le layer de transition du culling mask 
            collision.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID(LayerMask.LayerToName(collision.gameObject.layer)); 
        } 
    } 
 
    public void transferObject(ObjetTransfert objet) 
    { 
        objectsReceived.Add(objet); 
    } 
 
    private Vector2 FindProvenance(Transform objetTransform) 
    { 
        Vector2 direction = objetTransform.position - transform.position; 
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
            direction = direction.x > 0 ? Vector2.right : Vector2.left; 
        else 
            direction = direction.y > 0 ? Vector2.up : Vector2.down; 
 
        return direction; 
    }*/
}


public class ObjetTransfert
{
    public Collider2D collider { get; set; }
    public Vector2 provenance { get; set; }

    public ObjetTransfert(Collider2D collider, Vector2 provenance)
    {
        this.collider = collider;
        this.provenance = provenance;
    }
}