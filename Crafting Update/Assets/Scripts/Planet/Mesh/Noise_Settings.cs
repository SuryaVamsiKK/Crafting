using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Noise Settings", menuName = "Details/Noise")]
public class Noise_Settings : ScriptableObject
{
    [HideInInspector] public bool foldout;
    public NoiseType type;

    [Header("Noise : Pattern")]
    [Range(1,8)] public int numOfLayers = 1;
    public float strength = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistance = .5f;
    [ConditionalHide("type" , true)] public float weightMultiplyer;

    [Header("Nosie : Position")]
    public Vector3 center;
    public bool seaClamp = false;
    [ConditionalHide("seaClamp", true)] public float minValue;

}

