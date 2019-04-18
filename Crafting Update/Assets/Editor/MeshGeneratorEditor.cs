using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetGenerator))]
public class MeshGeneratorEditor : Editor
{
    PlanetGenerator myTarget;
    SerializedObject targetForProperties;
    Editor noiseEditor;
    bool enableBaseInspector = false;

    private void OnEnable()
    {
        myTarget = (PlanetGenerator)target;
        targetForProperties = serializedObject;
    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        myTarget.currentTab = GUILayout.Toolbar(myTarget.currentTab, new string[] { "Mesh", "LOD", "Shape" });

        switch (myTarget.currentTab)
        {
            case 0:
                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("shapeSettings.previewResolution"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("shapeSettings.realResolution"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("shapeSettings.planetRadius"));
                GUILayout.EndVertical();
                break;
            case 1:
                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("lodSettings.maxDepth"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("lodSettings.playerdetectionRadius"));
                GUILayout.EndVertical();
                break;
            case 2:
                for (int i = 0; i < myTarget.shapeSettings.noiseLayer.Count; i++)
                {
                    myTarget.shapeSettings.noiseLayer[i].enable = EditorGUILayout.Toggle("Enable Noise Layer " + i, myTarget.shapeSettings.noiseLayer[i].enable);

                    EditorGUI.BeginDisabledGroup(myTarget.shapeSettings.noiseLayer[i].enable == false);
                    GUILayout.BeginVertical("Box");
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    myTarget.shapeSettings.noiseLayer[i].noiseSettings.foldout = EditorGUILayout.Foldout(myTarget.shapeSettings.noiseLayer[i].noiseSettings.foldout, "Show Noise");
                    GUILayout.EndHorizontal();
                    if (myTarget.shapeSettings.noiseLayer[i].noiseSettings.foldout)
                    {
                        DrawSettingsEditor(myTarget.shapeSettings.noiseLayer[i].noiseSettings, myTarget.CreatePlanet, ref noiseEditor);
                    }
                    GUILayout.EndVertical();
                    EditorGUI.EndDisabledGroup();
                }
                //EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.materialName"));
                break;
            default:
                break;
        }

        if(EditorGUI.EndChangeCheck())
        {
            myTarget.CreatePlanet();
        }


        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Reconstruct"))
        {
            myTarget.CreatePlanet();
        }
        
        enableBaseInspector = EditorGUILayout.Toggle("Enable Base Inspector", enableBaseInspector);
        GUILayout.Space(10);

        if (enableBaseInspector)
        {
            base.OnInspectorGUI();
        }
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref Editor editor)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if (onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }
    }
}
