using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome Settings", menuName = "Details/Biome")]
public class Biome : ScriptableObject
{
    [HideInInspector] public bool foldout;
    public string BiomeName;
    public Gradient gradient;
    public Color tint;
    [Range(0, 1)] public float startHeight;
    [Range(0, 1)] public float tintPercent;

    public List<BiomeColorPalette> colorPalettes = new List<BiomeColorPalette>();
}