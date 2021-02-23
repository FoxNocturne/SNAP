using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ClignotementCollectable : MonoBehaviour
{
    private Light2D clignotement;
    private float t = 0;
    private float lS = 1;
    private bool transition = false;
    public GameObject bulle;
    bool delay = false;
    bool pickUp = false;
    PauseMenu afficherCollectable;

    CollectablesUI CollectableTrouve;


    // Start is called before the first frame update
    void Start()
    {
        clignotement = GetComponent<Light2D>();
        afficherCollectable = GameObject.Find("CanvasPause/Menus").GetComponent<PauseMenu>();
        

    }

    // Update is called once per frame
    void Update()
    {
        clignotement.intensity = Mathf.Lerp(0, 2, t);
        transform.localScale = new Vector3(Mathf.Lerp(0, 1, lS), Mathf.Lerp(0, 1, lS), Mathf.Lerp(0, 1, lS));
        if(transition)
        {
            t += Time.deltaTime;
            if(t >= 1)
            {
                transition = false;
            }
        }
        else if(!transition && !delay)
        {
            t -= Time.deltaTime;
            if(t <= 0 && !delay)
            {
                StartCoroutine(delayCligno());                    
            }
        }

        if(pickUp)
        {
            lS -= Time.deltaTime * 5;
            if(Input.GetButtonDown("Action"))
            {
                Time.timeScale = 0; 
                afficherCollectable.firstCollectableButton = afficherCollectable.allCollectableButton[GetComponent<ObserveThisThing>().Numero];
                afficherCollectable.CollectableMenu();
                afficherCollectable.CollectableInstance = true;
                CollectableTrouve = GameObject.Find("CanvasPause/CollectablesUI").GetComponent<CollectablesUI>();
                CollectableTrouve.TaskForDisplay(GetComponent<ObserveThisThing>().Numero);
            }
        }
    }

    IEnumerator delayCligno()
    {
        delay = true;
        yield return new WaitForSeconds(3);
        transition = true;
        delay = false;
    }

    public void AnimPickUp()
    {
        pickUp = true;

        Destroy(bulle);
        transition = true;
        gameObject.tag = "Untagged";
        Destroy(gameObject, 7);
    }    
}
