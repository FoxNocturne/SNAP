using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public WarpSpeed Transition;
    public bool TransitionTrigger = false;
    int dimension = 0; // 0 = Dictature // 1 = Chaos // 2 = Post_Apo
    public GameObject Dictature, Chaos, Post_Apo;
    AudioSource SonHero;
    float SNAP;
    public AudioClip Clac;

    void Start()
    {
        Transition.Mask.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        Transition.Mask2.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        Transition.SizeTransition = 12f;
        SonHero = GetComponent<AudioSource>();        
    }

    void Update()
    {        
        SNAP = Input.GetAxis("SNAP"); // La valeur SNAP est égal à 1 lorsqu'on presse le bouton (2) , égal à -1 lorsqu'on appuie sur le bouton (1) 
        if (Input.GetButtonDown("SNAP") && !TransitionTrigger && Transition.SizeTransition >= 12)
        {
            Transition.SizeTransition = 0f; // C'est la valeur qui permet de changer la taille du mask dans WarpSpeed
            Transition.Mask.maskInteraction = SpriteMaskInteraction.VisibleInsideMask; // Le cercle noir, elle masque directement le jeu pour voir le fond
            Transition.Mask2.maskInteraction = SpriteMaskInteraction.VisibleInsideMask; // Il s'agit des particules dans le fond

            StartCoroutine(TransitionEffect()); // Effectue l'animation
            // le numero de dimension active le gameObject de la dimension concerné.
            if (SNAP == 1)
            {
                dimension++;
                if (dimension > 2)
                {
                    dimension = 0;
                }
            }
            else if (SNAP == -1)
            {
                dimension--;
                if (dimension < 0)
                {
                    dimension = 2;
                }
            }           
        }

        if (TransitionTrigger)
        {
            if(Transition.SizeTransition < 13)
            {
                Transition.SizeTransition += Time.deltaTime * 30;
            }
            
        }
        else if (!TransitionTrigger)
        {

            if(Transition.Mask.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
            {
                
                Transition.Mask.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                Transition.Mask2.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                Transition.SizeTransition = 0f;
                
            }
            if(Transition.SizeTransition < 13)
            {
                Transition.SizeTransition += Time.deltaTime * 30;
            }
        }

        // Effectue l'animation
        IEnumerator TransitionEffect()
        {
            SonHero.PlayOneShot(Clac, 1f); // SON
            TransitionTrigger = true; // Active la transition
            GetComponent<Hero>().activeControl = false; // Désactive le héros dans le script hero (pour qu'il ne bouge plus)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            yield return new WaitForSeconds(1);
            switch (dimension)
            {
                case 0:
                    Dictature.SetActive(true);
                    Chaos.SetActive(false);
                    Post_Apo.SetActive(false);
                    break;
                case 1:
                    Dictature.SetActive(false);
                    Chaos.SetActive(true);
                    Post_Apo.SetActive(false);
                    break;
                case 2:
                    Dictature.SetActive(false);
                    Chaos.SetActive(false);
                    Post_Apo.SetActive(true);
                    break;
            }
           
            TransitionTrigger = false; // Désactive la transition pour que l'animation s'effectue en effet inverse
            GetComponent<Rigidbody2D>().gravityScale = 2;
            GetComponent<Hero>().activeControl = true;
        }

    }
}
