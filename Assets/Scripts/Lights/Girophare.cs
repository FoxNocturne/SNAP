using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Girophare : MonoBehaviour
{
    public float speed;
    public Color[] couleurs = new Color[2];

    private Light2D lightComp;
    private float maxRadius;
    public int direction = 0;
    private bool on = false;
    
    void Start()
    {
        lightComp = GetComponent<Light2D>();
        maxRadius = lightComp.pointLightOuterRadius;
    }

    void Update()
    {
        if (on)
        {
            lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius - (speed * Time.deltaTime), 0, maxRadius);
            on = !(lightComp.pointLightOuterRadius == 0);
        }
        else
        {
            lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius + (speed * Time.deltaTime), 0, maxRadius);
            on = lightComp.pointLightOuterRadius == maxRadius;
        }

        if(lightComp.pointLightOuterRadius == 0)
        {
            direction = direction == 0 ? 1 : 0;
            lightComp.color = couleurs[direction];
            transform.Rotate(0, 0, 180 * (direction == 1 ? -1 : 1));
        }
    }
}
