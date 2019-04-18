using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Palette Settings", menuName = "Details/Biome Color Palette")]
public class BiomeColorPalette : ScriptableObject
{
    public Color primary;
    public Color secondary;
    public Color tertiary;

    public BiomeColorPalette()
    {
        primary = Color.white;
        secondary = Color.white;
        tertiary = Color.white;
    }
}
