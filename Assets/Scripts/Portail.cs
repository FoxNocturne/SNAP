using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public float attractionForce = 1;

    [HideInInspector] public Portail portailLinked;

    Transform draggedObject;
    Vector2 defaultScale;
    float maxDistance, colliderMaxDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == draggedObject)
        {
            draggedObject.gameObject.layer = portailLinked.gameObject.layer;
            draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            draggedObject.localScale = defaultScale;
            draggedObject.tag = "Item";

            Destroy(portailLinked.gameObject);
            Destroy(gameObject);
        }

        if (!(collision.tag == "Item"))
            return;

        draggedObject = collision.transform;
        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        defaultScale = draggedObject.localScale;
        maxDistance = Mathf.Abs(Vector2.Distance(draggedObject.position, transform.position));
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
    }

    void OnDestroy()
    {
        if (draggedObject == null) return;
        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        draggedObject.localScale = defaultScale;
        draggedObject.tag = "Item";
    }

}