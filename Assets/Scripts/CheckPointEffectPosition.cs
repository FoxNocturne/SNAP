using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffectPosition : MonoBehaviour
{
    // Start is called before the first frame update
    Transform heros;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    SpriteRenderer flash;

    Shader heroShader;
    // Update is called once per frame
    void Start()
    {
        
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        heros = GameObject.FindWithTag("Player").transform;
        flash = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        StartCoroutine(Effect());
    }
    void Update()
    {
        transform.position = new Vector2(heros.position.x, heros.position.y - 1.6f);
    }

    IEnumerator Effect()
    {
        flash.material.shader = shaderGUItext;
        yield return new WaitForSeconds(0.2f);
        flash.material.shader = shaderSpritesDefault;
    }
}
