using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Details/Resource")]
public class Resource : ScriptableObject
{
    [Header("Resource Details")]
    public ResourceType m_Resource;
    public ResourceForm m_TypeOfReource;

    [Header("Material Details")]
    public Color m_ColorCode;
    [Range(0, 1)] public float smoothness;
    [Range(0, 1)] public float metallic;
}

public enum ResourceType
{
    Carbon = 0, Zinc, Copper, Null
}

public enum ResourceForm
{
    Ore, Refined, Composit
}


