using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject[] Elements;
    public GameObject[] plan1;
    public GameObject[] plan2;
    public GameObject[] plan1P;
    public GameObject[] plan2P;
    public Sprite[] city;
    public Animator EcranNoir;
    private int dimension = 0; // 0 = Dictature // 1 = Chaos // 2 = Post-Apo

    public Color[] ColorDictature;
    public Color[] ColorChaos;
    public Color[] ColorPostApo;
    public Color[] Fond;
    public SpriteRenderer Background;
    public Color transparent;    
    private float timeMax = 10;
    private float timeChange = 3; 
    
    void Start()
    {
                Elements[0].SetActive(true);
                Elements[1].SetActive(false);
                Elements[2].SetActive(false);
        plan1P[0].GetComponent<SpriteRenderer>().color = transparent;
        plan1P[1].GetComponent<SpriteRenderer>().color = transparent;
        plan2P[0].GetComponent<SpriteRenderer>().color = transparent;
        plan2P[1].GetComponent<SpriteRenderer>().color = transparent;  
        timeChange = timeMax;
    }


    // Update is called once per frame
    void Update()
    {                
        if(timeChange <= 0)
        {
            timeChange = timeMax;
            EcranNoir.SetTrigger("transition");
        }
        else
        {
            timeChange -= Time.deltaTime;
        }
        
        // DICTATURE // CHAOS //
        if(dimension == 0 || dimension == 1)
        {
            plan1[0].transform.Translate(Vector2.left * Time.deltaTime * 3);
            plan1[1].transform.Translate(Vector2.left * Time.deltaTime * 3);

            if(plan1[0].transform.position.x <= -32f)
            {
                plan1[0].transform.position = new Vector2(22, plan1[0].transform.position.y);
            }
            if(plan1[1].transform.position.x <= -32f)
            {
                plan1[1].transform.position = new Vector2(22, plan1[1].transform.position.y);
            }

            plan2[0].transform.Translate(Vector2.left * Time.deltaTime * 2);
            plan2[1].transform.Translate(Vector2.left * Time.deltaTime * 2);

            if(plan2[0].transform.position.x <= -30f)
            {
                plan2[0].transform.position = new Vector2(24, plan2[0].transform.position.y);
            }
            if(plan2[1].transform.position.x <= -30f)
            {
                plan2[1].transform.position = new Vector2(24, plan2[1].transform.position.y);
            }
        }
        else
        {
            // POST-APOCALYPSE //
            plan1P[0].transform.Translate(Vector2.left * Time.deltaTime * 3);
            plan1P[1].transform.Translate(Vector2.left * Time.deltaTime * 3);

            if(plan1P[0].transform.position.x <= -32f)
            {
                plan1P[0].transform.position = new Vector2(22, plan1P[0].transform.position.y);
            }
            if(plan1P[1].transform.position.x <= -32f)
            {
                plan1P[1].transform.position = new Vector2(22, plan1P[1].transform.position.y);
            }

            plan2P[0].transform.Translate(Vector2.left * Time.deltaTime * 2);
            plan2P[1].transform.Translate(Vector2.left * Time.deltaTime * 2);

            if(plan2P[0].transform.position.x <= -30f)
            {
                plan2P[0].transform.position = new Vector2(24, plan2P[0].transform.position.y);
            }
            if(plan2P[1].transform.position.x <= -30f)
            {
                plan2P[1].transform.position = new Vector2(24, plan2P[1].transform.position.y);
            }
        }
    }
    public void TransitionDimension()
    {
        dimension++;
        if(dimension > 2)
        {
            dimension = 0;
        }
        switch(dimension)
        {
            case 0 :                
            Background.color = Fond[0];
            Elements[0].SetActive(true);
            Elements[1].SetActive(false);
            Elements[2].SetActive(false);
            plan1P[0].GetComponent<SpriteRenderer>().color = transparent;
            plan1P[1].GetComponent<SpriteRenderer>().color = transparent;
            plan2P[0].GetComponent<SpriteRenderer>().color = transparent;
            plan2P[1].GetComponent<SpriteRenderer>().color = transparent;  

            plan1[0].GetComponent<SpriteRenderer>().sprite = city[0];
            plan1[1].GetComponent<SpriteRenderer>().sprite = city[0];
            plan2[0].GetComponent<SpriteRenderer>().sprite = city[0];
            plan2[1].GetComponent<SpriteRenderer>().sprite = city[0];

            plan1[0].GetComponent<SpriteRenderer>().color = ColorDictature[0];
            plan1[1].GetComponent<SpriteRenderer>().color = ColorDictature[0];
            plan2[0].GetComponent<SpriteRenderer>().color = ColorDictature[1];
            plan2[1].GetComponent<SpriteRenderer>().color = ColorDictature[1];
            break;

            case 1 :
            Background.color = Fond[1];
            Elements[0].SetActive(false);
            Elements[1].SetActive(true);
            Elements[2].SetActive(false);    
            plan1[0].GetComponent<SpriteRenderer>().sprite = city[1];
            plan1[1].GetComponent<SpriteRenderer>().sprite = city[1];
            plan2[0].GetComponent<SpriteRenderer>().sprite = city[1];
            plan2[1].GetComponent<SpriteRenderer>().sprite = city[1];

            plan1[0].GetComponent<SpriteRenderer>().color = ColorChaos[0];
            plan1[1].GetComponent<SpriteRenderer>().color = ColorChaos[0];
            plan2[0].GetComponent<SpriteRenderer>().color = ColorChaos[1];
            plan2[1].GetComponent<SpriteRenderer>().color = ColorChaos[1];                
            break;

            case 2 :
            Background.color = Fond[2];
            Elements[0].SetActive(false);
            Elements[1].SetActive(false);
            Elements[2].SetActive(true);
            plan1[0].GetComponent<SpriteRenderer>().color = transparent;
            plan1[1].GetComponent<SpriteRenderer>().color = transparent;
            plan2[0].GetComponent<SpriteRenderer>().color = transparent;
            plan2[1].GetComponent<SpriteRenderer>().color = transparent;

            plan1P[0].GetComponent<SpriteRenderer>().color = ColorPostApo[0];
            plan1P[1].GetComponent<SpriteRenderer>().color = ColorPostApo[0];
            plan2P[0].GetComponent<SpriteRenderer>().color = ColorPostApo[1];
            plan2P[1].GetComponent<SpriteRenderer>().color = ColorPostApo[1];                               
            break;     
        }
    }    
}
