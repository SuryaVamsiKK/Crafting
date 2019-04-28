using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInputs : MonoBehaviour
{
    [Header("Shape")]
    public float radius = 200f;
    public Shape_Settings surfaceShapeSettings;
    public Shape_Settings seaShapeSettings;

    [Header("LOD")]
    public LOD_Settings lodSettings;

    [Header("Color")]
    public ColorSettings surfaceColorSettings;
    public ColorSettings seaColorSettings;

    public Transform sky;

    void OnValidate()
    {
        surfaceShapeSettings.planetRadius = radius;
        seaShapeSettings.planetRadius = radius + (radius * 0.25f);

        GameObject surface = transform.GetChild(0).gameObject;
        GameObject sea = transform.GetChild(1).gameObject;

        surface.GetComponent<PlanetGenerator>().lodSettings = lodSettings;
        surface.GetComponent<PlanetGenerator>().shapeSettings = surfaceShapeSettings;
        surface.GetComponent<ColorGenerator>().settings = surfaceColorSettings;
        sea.GetComponent<PlanetGenerator>().lodSettings = lodSettings;
        sea.GetComponent<PlanetGenerator>().shapeSettings = seaShapeSettings;
        sea.GetComponent<ColorGenerator>().settings = seaColorSettings;

        sky.localScale = new Vector3(1000 + radius, 1000 + radius, 1000 + radius);
        GetComponent<Attractor>().gravitationalField = 1500 + radius;

        GeneratePlanet();
    }

    public void GeneratePlanet()
    {
        transform.GetChild(0).GetComponent<PlanetGenerator>().CreatePlanet();
        transform.GetChild(1).GetComponent<PlanetGenerator>().CreatePlanet();
    }
}
