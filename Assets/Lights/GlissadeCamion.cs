using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlissadeCamion : MonoBehaviour
{
    public float speed = 0.2f;
    public List<Transform> listLights = new List<Transform>();

    private int index = 0;

    void Start()
    {
        foreach (var light in listLights)
            light.localScale = Vector2.zero;

        listLights[0].localScale = Vector2.one;
    }

    void Update()
    {
        int nextIndex = (index + 1) % 6;
        listLights[index].localScale = new Vector2(Mathf.Clamp(listLights[index].localScale.x - speed * Time.deltaTime, 0, 1), Mathf.Clamp(listLights[index].localScale.y - speed * Time.deltaTime, 0, 1));
        listLights[nextIndex].localScale = new Vector2(Mathf.Clamp(listLights[nextIndex].localScale.x + speed * Time.deltaTime, 0, 1), Mathf.Clamp(listLights[nextIndex].localScale.y + speed * Time.deltaTime, 0, 1));

        if (listLights[index].localScale.x == 0)
            index = nextIndex;
    }
}
