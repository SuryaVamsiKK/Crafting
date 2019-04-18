using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shape_Settings
{
    [Range(1, 256)] public int previewResolution = 10;
    [Range(1, 256)] public int realResolution = 10;
    public float planetRadius;
    [Header("Noise")] public List<NoiseLayer> noiseLayer;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enable = true;
        [ConditionalHide("enable")]
        public Noise_Settings noiseSettings;
    }
}
