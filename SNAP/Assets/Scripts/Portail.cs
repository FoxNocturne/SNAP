using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public int targetDimension;
    public GameObject portalLinked;

    private List<Collider2D> arrived = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Item") && !arrived.Contains(collision))
        {
            StopAllCoroutines();
            StartCoroutine(portailScale(collision.transform.localScale / 1.5f));
            portalLinked.GetComponent<Portail>().transferObject(collision, collision.transform.localScale / 1.5f);

            collision.gameObject.layer = targetDimension + 9;
            
        }else if((collision.tag == "Player") && !arrived.Contains(collision))
        {
            StopAllCoroutines();
            StartCoroutine(portailScale(collision.transform.localScale / 1.5f));
            portalLinked.GetComponent<Portail>().transferObject(collision, collision.transform.localScale / 1.5f);

            collision.GetComponent<Snap>().ActiveSnap(targetDimension == collision.GetComponent<Snap>().GetNextDimension() ? 1 : -1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (arrived.Contains(collision))
        {
            arrived.Remove(collision);
        }
    }

    public void transferObject(Collider2D collision, Vector3 targetScale)
    {
        arrived.Add(collision);
        StopAllCoroutines();
        StartCoroutine(portailScale(targetScale));
    }

    private IEnumerator portailScale(Vector3 targetScale)
    {
        Vector3 originScale = transform.localScale;

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
