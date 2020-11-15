using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODIR : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -42.25985f, float.MaxValue));
    }
}
