using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorGenerator))]
public class ColorGeneratorEditor : Editor
{
    ColorGenerator myTarget;
    SerializedObject targetForProperties;
    Editor biomeEditor;
    Editor noiseEditor;
    bool enableBaseInspector = false;

    private void OnEnable()
    {
        myTarget = (ColorGenerator)target;
        targetForProperties = serializedObject;
    }

    public override void OnInspectorGUI()
    {
        myTarget.currentTab = GUILayout.Toolbar(myTarget.currentTab, new string[] { "Biomes", "Noise", "Material Properties" });
        EditorGUI.BeginChangeCheck();

        switch (myTarget.currentTab)
        {
            case 0:
                for (int i = 0; i < myTarget.settings.biomeColorSettings.biomes.Length; i++)
                {
                    GUILayout.BeginVertical("Box");
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    myTarget.settings.biomeColorSettings.biomes[i].foldout = EditorGUILayout.Foldout(myTarget.settings.biomeColorSettings.biomes[i].foldout, "Show Biome");
                    GUILayout.EndHorizontal();
                    if (myTarget.settings.biomeColorSettings.biomes[i].foldout)
                    {
                        DrawSettingsEditor(myTarget.settings.biomeColorSettings.biomes[i], myTarget.UpdateColorSettings, ref biomeEditor);
                    }
                    GUILayout.EndVertical();
                }
                break;
            case 1:
                GUILayout.BeginVertical("Box");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                myTarget.settings.biomeColorSettings.noise.foldout = EditorGUILayout.Foldout(myTarget.settings.biomeColorSettings.noise.foldout, "Show Noise");
                GUILayout.EndHorizontal();
                if (myTarget.settings.biomeColorSettings.noise.foldout)
                {
                    DrawSettingsEditor(myTarget.settings.biomeColorSettings.noise, myTarget.UpdateColorSettings, ref noiseEditor);
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.biomeColorSettings.noiseOffset"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.biomeColorSettings.noiseStrengeth"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.biomeColorSettings.blendAmount"));
                GUILayout.EndVertical();
                break;
            case 2 :

                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.materialName"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.shader"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.specular"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("settings.smoothness"));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("material"));
                EditorGUILayout.PropertyField(targetForProperties.FindProperty("texture"));
                GUILayout.EndVertical();
                break;
            default:
                break;
        }

        if(EditorGUI.EndChangeCheck())
        {
            myTarget.UpdateColorSettings();
        }

        serializedObject.ApplyModifiedProperties();
        
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
