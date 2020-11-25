using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public int targetDimension;
    public Portail portailLinked;

    private List<Collider2D> objectsReceived = new List<Collider2D>();
    private GameObject[] cameras = new GameObject[3];

    private void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectsReceived.Contains(collision))
        {
            Debug.Log($"Portail {LayerMask.LayerToName(gameObject.layer)} : Un objet entre");
            portailLinked.transferObject(collision);

            if (collision.tag == "Item")
            {
                Debug.Log($"Portail {LayerMask.LayerToName(gameObject.layer)} : C'est un item");
                collision.gameObject.layer = targetDimension + 12;
                cameras[targetDimension].GetComponent<Camera>().cullingMask |= 1 << targetDimension + 12;
            }
            else if (collision.tag == "Player")
            {
                collision.GetComponent<Snap>().ActiveSnap(targetDimension == collision.GetComponent<Snap>().GetNextDimension() ? 1 : -1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsReceived.Contains(collision))
        {
            Debug.Log($"Portail {LayerMask.LayerToName(gameObject.layer)} : L'objet sort");
            if (collision.tag == "Item")
                collision.gameObject.layer = portailLinked.targetDimension + 9;
            objectsReceived.Remove(collision);
            cameras[portailLinked.targetDimension].GetComponent<Camera>().cullingMask &= ~(1 << portailLinked.targetDimension + 12);
        }
    }

    public void transferObject(Collider2D collision)
    {
        Debug.Log($"Portail {LayerMask.LayerToName(gameObject.layer)} : On reçoit l'objet");
        objectsReceived.Add(collision);
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