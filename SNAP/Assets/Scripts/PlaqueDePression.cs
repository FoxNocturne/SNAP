using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public List<Transform> portes;
    public float speed = 10f;
    [Range(0, 1)]
    public float ouverture = 0.9f;

    private List<Vector2> portesPos = new List<Vector2>();
    private List<Collider2D> actualColliders = new List<Collider2D>();
    private int actualCollidersOffset;

    private void Start()
    {
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);
        actualCollidersOffset = actualColliders.Count;

        foreach(var porte in portes)
        {
            portesPos.Add(porte.position);
        }
    }

    private void Update()
    {
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), actualColliders);

        if (actualColliders.Count - actualCollidersOffset == 0)
        {
            foreach (var porte in portes)
            {
                porte.transform.position = Vector2.MoveTowards(porte.position, portesPos[portes.IndexOf(porte)], speed * Time.deltaTime);
            }
        }
        else
        {
            foreach (var porte in portes)
            {
                Vector2 target = new Vector2(porte.position.x, portesPos[portes.IndexOf(porte)].y + porte.GetComponent<BoxCollider2D>().size.y * porte.transform.localScale.y * 0.9f);
                porte.transform.position = Vector2.MoveTowards(porte.position, target, speed * Time.deltaTime);
            }
        }
    }
}
