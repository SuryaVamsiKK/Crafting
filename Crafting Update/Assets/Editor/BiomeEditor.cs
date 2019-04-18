using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Biome))]
public class BiomeEditor : Editor
{
    Biome myTarget;
    SerializedObject targetForProperties;
    Editor noiseEditor;
    bool enableBaseInspector = false;
    GUIStyle centerBoldFont;
    GUIStyle centeritalicFont;

    private void OnEnable()
    {
        myTarget = (Biome)target;
        targetForProperties = serializedObject;
    }

    public override void OnInspectorGUI()
    {
        centerBoldFont = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.BoldAndItalic, fontSize = 13 };
        centeritalicFont = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };


        if (enableBaseInspector)
        {
            base.OnInspectorGUI();
        }
        EditorGUI.BeginChangeCheck();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(targetForProperties.FindProperty("BiomeName"));
        EditorGUILayout.PropertyField(targetForProperties.FindProperty("gradient"));
        EditorGUILayout.PropertyField(targetForProperties.FindProperty("tint"));        
        myTarget.startHeight = EditorGUILayout.Slider("Start Height",myTarget.startHeight, 0, 1);
        myTarget.tintPercent = EditorGUILayout.Slider("Tint percent",myTarget.tintPercent, 0, 1);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        if (myTarget.colorPalettes.Count > 0)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Color Palettes", centerBoldFont, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            for (int i = 0; i < myTarget.colorPalettes.Count; i++)
            {
                SerializedObject tests = new SerializedObject(myTarget.colorPalettes[i]);

                GUILayout.BeginVertical("Box");
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Color Palette " + (i + 1).ToString(), centeritalicFont, GUILayout.ExpandWidth(true));
                GUILayout.EndHorizontal();

                myTarget.colorPalettes[i] = EditorGUILayout.ObjectField("", myTarget.colorPalettes[i], typeof(BiomeColorPalette), false) as BiomeColorPalette;

                EditorGUILayout.PropertyField(tests.FindProperty("primary"));
                EditorGUILayout.PropertyField(tests.FindProperty("secondary"));
                EditorGUILayout.PropertyField(tests.FindProperty("tertiary"));

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Randomize"))
                {
                    myTarget.colorPalettes[i].primary = RandomizeColor();
                    myTarget.colorPalettes[i].secondary = RandomizeColor();
                    myTarget.colorPalettes[i].tertiary = RandomizeColor();
                }

                if (GUILayout.Button("Remove"))
                {
                    myTarget.colorPalettes.RemoveAt(i);
                    AssetDatabase.DeleteAsset("Assets/Data/Biomes/Color Palettes/" + myTarget.name + " (" + (i + 1) + ").asset");
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    tests.ApplyModifiedProperties();
                }      
                
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();


                if (i < myTarget.colorPalettes.Count - 1)
                {
                    GUILayout.Space(10f);
                }
            }
        }

        if (myTarget.colorPalettes.Count > 0)
        {
            GUILayout.Space(10f);
        }
        if (GUILayout.Button("Add"))
        {
            BiomeColorPalette palette = ScriptableObject.CreateInstance<BiomeColorPalette>();
            myTarget.colorPalettes.Add(palette);
            AssetDatabase.CreateAsset(palette, "Assets/Data/Biomes/Color Palettes/" + myTarget.name + " (" + myTarget.colorPalettes.Count.ToString() + ").asset");
            AssetDatabase.SaveAssets();
        }
        GUILayout.EndVertical();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    Color RandomizeColor()
    {
        return Random.ColorHSV();
    }
}
