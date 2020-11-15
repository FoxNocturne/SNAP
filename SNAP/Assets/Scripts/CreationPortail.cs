using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationPortail : MonoBehaviour
{
    public GameObject portailPrefab;
    public Color[] portailColors = new Color[3];

    private GameObject portailActualDimension, portailTargetDimension;
    private Snap snapScript;

    void Start()
    {
        snapScript = GetComponent<Snap>();    
    }

    void Update()
    {
        float portailInput = Input.GetAxis("Portail");

        if (Input.GetButtonDown("Portail"))
        {
            CreerPortail(portailInput == 1 ? snapScript.GetNextDimension() : snapScript.GetPreviousDimension());
        }
    }

    private void CreerPortail(int dimensionCible)
    {
        if (portailActualDimension)
        {
            Destroy(portailActualDimension);
            Destroy(portailTargetDimension);
        }

        portailActualDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailActualDimension.GetComponent<SpriteRenderer>().color = portailColors[dimensionCible];
        portailActualDimension.layer = snapScript.GetActualDimension() + 9;
        portailActualDimension.GetComponent<Portail>().targetDimension = dimensionCible;
        
        portailTargetDimension = Instantiate(portailPrefab, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
        portailTargetDimension.GetComponent<SpriteRenderer>().color = portailColors[snapScript.GetActualDimension()];
        portailTargetDimension.layer = dimensionCible + 9;
        portailTargetDimension.GetComponent<Portail>().targetDimension = snapScript.GetActualDimension();

        portailActualDimension.GetComponent<Portail>().portalLinked = portailTargetDimension;
        portailTargetDimension.GetComponent<Portail>().portalLinked = portailActualDimension;
        Physics2D.SetLayerCollisionMask(12, LayerMask.GetMask("Character", LayerMask.LayerToName(snapScript.GetActualDimension() + 9), LayerMask.LayerToName(dimensionCible + 9)));
    }
}
    