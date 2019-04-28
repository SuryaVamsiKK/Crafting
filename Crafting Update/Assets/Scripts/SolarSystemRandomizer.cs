using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemRandomizer : MonoBehaviour
{

    public PlanetInputs[] planets;
    public int seed;
    public bool randomize;

    public Biome[] surfaceBiomes;
    public Biome[] seaBiomes;

    private void Awake()
    {
        randomizecenter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void randomizecenter()
    {
        Random.InitState(seed);
        for (int i = 0; i < planets.Length; i++)
        {
            for (int j = 0; j < planets[i].surfaceShapeSettings.noiseLayer.Count; j++)
            {
                planets[i].surfaceShapeSettings.noiseLayer[j].noiseSettings.center = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
                planets[i].GeneratePlanet();
            }

            for (int j = 0; j < planets[i].seaShapeSettings.noiseLayer.Count; j++)
            {
                planets[i].seaShapeSettings.noiseLayer[j].noiseSettings.center = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
                planets[i].GeneratePlanet();
            }

            for (int j = 0; j < planets[i].surfaceColorSettings.biomeColorSettings.biomes.Length; j++)
            {
                planets[i].surfaceColorSettings.biomeColorSettings.biomes[j] = surfaceBiomes[Random.Range(0, surfaceBiomes.Length)];
                planets[i].GeneratePlanet();
            }

            for (int j = 0; j < planets[i].seaColorSettings.biomeColorSettings.biomes.Length; j++)
            {
                planets[i].seaColorSettings.biomeColorSettings.biomes[j] = seaBiomes[Random.Range(0, seaBiomes.Length)];
                planets[i].GeneratePlanet();
            }
        }
    }

    private void OnValidate()
    {
        randomizecenter();
    }

    public void UpdateSeed()
    {
        seed++;
        randomizecenter();
    }
}
