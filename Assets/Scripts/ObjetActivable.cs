using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetActivable : MonoBehaviour
{
    protected List<GameObject> activators = new List<GameObject>();

    public abstract void Activation(GameObject activator);
    public abstract void Desactivation(GameObject activator);
}
