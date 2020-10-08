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
        SNAP = Input.GetAxis("SNAP");
        if (Input.GetButtonDown("SNAP") && !TransitionTrigger && Transition.SizeTransition >= 12)
        {
            Transition.SizeTransition = 0f;
            Transition.Mask.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            Transition.Mask2.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            StartCoroutine(TransitionEffect());
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


        IEnumerator TransitionEffect()
        {
            SonHero.PlayOneShot(Clac, 1f);
            TransitionTrigger = true;
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
           
            TransitionTrigger = false;
        }

    }
}
