using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ClignotementCollectable : MonoBehaviour
{
    private Light2D clignotement;
    private float t = 0;
    private bool transition = false;
    Color transparence;
    bool delay = false;

    bool pickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        clignotement = GetComponent<Light2D>();
        transparence = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        clignotement.intensity = Mathf.Lerp(0, 2, t);

        if(pickUp)
        {
                    Debug.Log(t);
            transparence = new Color(1, 1, 1, t);
        }

        if(!pickUp)
        {
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
        }
        else
        {
            if(transition)
            {
                t += Time.deltaTime * 5;
                if(t >= 1)
                {
                    transition = false;
                }
            }
            else if(!transition)
            {
                
                if(t > 0)
                {
                    t -= Time.deltaTime * 2;
                    transparence = new Color(1, 1, 1, t);

                }
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
        transition = true;
        t = 0;
        gameObject.tag = "Untagged";
        Destroy(gameObject, 2);
    }    
}
