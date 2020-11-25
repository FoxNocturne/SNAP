using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public int targetDimension;
    public Portail portailLinked;

    private List<ObjetTransfert> objectsReceived = new List<ObjetTransfert>();
    private GameObject[] cameras = new GameObject[3];

    private void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectsReceived.Find(objet => objet.collider == collision) == null)
        {
            portailLinked.transferObject(new ObjetTransfert(collision, FindProvenance(collision.transform)));

            if (collision.tag == "Item")
            {
                collision.gameObject.layer = targetDimension + 12;
                cameras[portailLinked.targetDimension].GetComponent<Camera>().cullingMask |= 1 << targetDimension + 12; // Ajoute le layer de transition au culling mask de la dimension de départ
            }
            else if (collision.tag == "Player")
            {
                collision.GetComponent<Snap>().ActiveSnap(targetDimension == collision.GetComponent<Snap>().GetNextDimension() ? 1 : -1);
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
    }
}
 /*   private IEnumerator portailScale(Vector3 targetScale)
    {
        Vector3 originScale = Vector3.one / 2;

        float debut = Time.time;
        while(transform.localScale.x < targetScale.x || transform.localScale.y < targetScale.y)
        {
            if (Time.time > debut + 1)
                break;

            transform.localScale = new Vector2(transform.localScale.x + (targetScale.x - transform.localScale.x) * Time.deltaTime * 30,
                                               transform.localScale.y + (targetScale.y - transform.localScale.y) * Time.deltaTime * 30);
            yield return new WaitForSeconds(0.05f);
        }

        while (transform.localScale.x > originScale.x || transform.localScale.y > originScale.y)
        {
            transform.localScale = new Vector2(transform.localScale.x + (originScale.x - transform.localScale.x) * Time.deltaTime * 30,
                                               transform.localScale.y + (originScale.y - transform.localScale.y) * Time.deltaTime * 30);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
//*/

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