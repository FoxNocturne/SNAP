using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlotPolice : MonoBehaviour
{
    public float speed = 5;

    private Light2D lightComp;
    private float maxOuterRadius, innerRadius;
    private bool on = true;

    void Start()
    {
        lightComp = GetComponent<Light2D>();
        maxOuterRadius = lightComp.pointLightOuterRadius;
        innerRadius = lightComp.pointLightInnerRadius;
    }
    
    void Update()
    {
        if (on)
        {
            lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius - (speed * Time.deltaTime), innerRadius, maxOuterRadius);
            on = !(lightComp.pointLightOuterRadius == innerRadius);
        }
        else
        {
            lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius + (speed * Time.deltaTime), innerRadius, maxOuterRadius);
            on = lightComp.pointLightOuterRadius == maxOuterRadius;
        }
    }
}
