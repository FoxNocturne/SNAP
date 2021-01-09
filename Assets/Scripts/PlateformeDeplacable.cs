using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeDeplacable : ObjetActivable
{
    public float speed = 10f;
    [Min(0)]
    public float distance = 2f;

    public DirectionEnum direction;

    private Vector2 startingPos;
    private Vector2 targetPos;
    private bool activated = false;

    void Start()
    {
        startingPos = transform.position;

        switch (direction)
        {
            case DirectionEnum.Haut:
                targetPos = new Vector2(startingPos.x, startingPos.y + distance);
                break;

            case DirectionEnum.Bas:
                targetPos = new Vector2(startingPos.x, startingPos.y - distance);
                break;

            case DirectionEnum.Gauche:
                targetPos = new Vector2(startingPos.x - distance, startingPos.y);
                break;

            case DirectionEnum.Droite:
                targetPos = new Vector2(startingPos.x + distance, startingPos.y);
                break;
        }
    }

    private void Update()
    {
        if(activated)
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, startingPos, speed * Time.deltaTime);
    }

    public override void Activation()
    {
        activated = true;
    }

    public override void Desactivation()
    {
        activated = false;
    }
}

public enum DirectionEnum
{
    Haut, Bas, Gauche, Droite
}