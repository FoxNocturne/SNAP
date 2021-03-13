using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBar_Test : MonoBehaviour
{
    public Scrollbar bar;
    float moy;
    public CollectablesUI nbCollectables;
 
    public void Lancement(int value)
    {
        Debug.Log("dslm");
        StartCoroutine(Scroll(value));
       
    }
    public IEnumerator Scroll(float value)
    {
        yield return null; // Waiting just one frame is probably good enough, yield return null does that
        
        moy = (14  - value) / 14 ;
        bar.value = moy;

    }
}
