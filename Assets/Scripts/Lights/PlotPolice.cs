using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlotPolice : MonoBehaviour
{
    public float speed = 5;

    private Light2D lightComp;
    private float maxRadius;
    private bool on = true;

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
    }
}
