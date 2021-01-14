using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserveThisThing : MonoBehaviour
{
    // Ajouter manuellement sur Unity le texte et l'image à afiicher lorsque le personnage interagit avec un panneau ou une affiche
    public GameObject Observe;
    public Sprite Object_Picture;
    public string text;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Debug.Log(Time.deltaTime);
            // RAMASSER UN OBJET
            if (Input.GetButtonDown("Attraper"))
            {
                if (GameObject.Find("ObserveThisThing") == null)
                {
                    GameObject DisplayObject = Instantiate(Observe, transform.position, Quaternion.identity);
                    DisplayObject.name = "ObserveThisThing";
                    DisplayObject.transform.GetChild(0).GetComponent<Image>().sprite = Object_Picture;
                    DisplayObject.transform.GetChild(1).GetComponent<Text>().text = text;
                    Time.timeScale = 0;
                }
            }
        }
    }

}