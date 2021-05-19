using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraEditor : Editor
{
    CameraController Camera;
    private bool scriptActive;

    public override void OnInspectorGUI()
    {
        Camera = (CameraController)target;
        DrawDefaultInspector();
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Look at player"))
        {
            if (!scriptActive)
            {
                Camera.LookAtPlayer();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
