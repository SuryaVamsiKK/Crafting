using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome Settings", menuName = "Details/Foilage")]
public class foilageObjects : ScriptableObject
{
    public GameObject tree;
    public BiomeColorPalette[] colorPalettes;
    public float spwanablility;
    public elevationPointLock height;

    [System.Serializable]
    public class elevationPointLock
    {
        public float minHeight;
        public float maxHeight;
    }
}
