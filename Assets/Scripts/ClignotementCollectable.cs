using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ClignotementCollectable : MonoBehaviour
{
    private Light2D clignotement;
    private float t = 0;
    private float lS = 1;
    private bool transition = false;
    public GameObject bulle;
    public GameObject MessageCollectable;
    bool delay = false;
    bool pickUp = false;
    PauseMenu afficherCollectable;
    GameObject message;

    CollectablesUI CollectableTrouve;

    AudioSource VoixLecture;
    public AudioClip CollectableRamasse;
    AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        clignotement = GetComponent<Light2D>();
        afficherCollectable = GameObject.Find("CanvasPause/Menus").GetComponent<PauseMenu>();
        audiosource = GetComponent<AudioSource>();

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
                CollectableTrouve.CollectableInstance = true;
                //CollectableTrouve.UpdateCollectables();             
                if(GetComponent<ObserveThisThing>().Numero == 5 || GetComponent<ObserveThisThing>().Numero == 6)
                {
                    CollectableTrouve.DisplayMomentLecture(GetComponent<ObserveThisThing>().Numero);
                } 
                Destroy(message);
                Destroy(gameObject);                                
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
        if (GameObject.Find("MessageCollectable") != null)
        {
            
            Destroy(GameObject.Find("MessageCollectable"));
        }
        audiosource.PlayOneShot(CollectableRamasse, 1f);
        message = Instantiate(MessageCollectable, transform.position, Quaternion.identity) as GameObject;
        message.name = "MessageCollectable";
        int numero = GetComponent<ObserveThisThing>().Numero;
        string nom = GetComponent<ObserveThisThing>().NomCollectable;
        PlayerPrefs.SetInt(nom, numero);
        message.GetComponentInChildren<Text>().text = "Vous avez découvert un indice : \n" + nom;

        StartCoroutine(TempsMessageCollectable());

        pickUp = true;

        Destroy(bulle);
        transition = true;
        gameObject.tag = "Untagged";
        Destroy(gameObject, 7);
    }

    IEnumerator TempsMessageCollectable()
    {
        yield return new WaitForSeconds(7.1f);
        Destroy(message);
    }        
}
