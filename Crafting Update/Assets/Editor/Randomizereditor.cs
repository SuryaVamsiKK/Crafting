using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Randomizer))]
public class Randomizereditor : Editor
{
    Randomizer myTarget;
    SerializedObject targetForProperties;
    float value;

    private void OnEnable()
    {
        myTarget = (Randomizer)target;
        targetForProperties = serializedObject;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.PropertyField(targetForProperties.FindProperty("seed"));
        EditorGUILayout.PropertyField(targetForProperties.FindProperty("noOfNumbers"));
        EditorGUILayout.PropertyField(targetForProperties.FindProperty("debug"));

        if (GUILayout.Button("Generate"))
        {
            if (myTarget.noOfNumbers > 1)
            {
                myTarget.GenerateNNumbers();
            }
            else
            {
                myTarget.GenerateSingleNumber();
            }
        }


        serializedObject.ApplyModifiedProperties();
    }
}
