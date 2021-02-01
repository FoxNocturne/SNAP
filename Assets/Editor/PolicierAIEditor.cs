using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor script pour la visualisation de la vision des poiciers dans l'inspecteur
[CustomEditor (typeof(PolicierAI))]
public class PolicierAIEditor : Editor
{
    private void OnSceneGUI()
    {
        PolicierAI ai = (PolicierAI)target;
        Handles.color = Color.grey;
        Handles.DrawWireArc(ai.transform.position, Vector3.forward, Vector3.left, 360, ai.viewDistance);

        Vector3 angleA = ai.DirFromAngle(90 + ai.viewAngle / 2, false);
        Vector3 angleB = ai.DirFromAngle(90 - ai.viewAngle / 2, false);
        
        Handles.DrawLine(ai.transform.position, ai.transform.position + angleA * ai.viewDistance);
        Handles.DrawLine(ai.transform.position, ai.transform.position + angleB * ai.viewDistance);
    }
}
