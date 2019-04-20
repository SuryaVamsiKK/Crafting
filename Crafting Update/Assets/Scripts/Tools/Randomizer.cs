using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [HideInInspector] public int seed;
    [HideInInspector] public int noOfNumbers;
    [HideInInspector] public bool debug;

    public void SetSeed(int seed)
    {
        Random.InitState(seed);
    }

    public float GenerateSingleNumber(int seed)
    {
        SetSeed(seed);
        float n = Random.Range(1f, 100f);
        PrintRandom(n);
        return n;
    }
    public float GenerateSingleNumber()
    {
        float n = Random.Range(1f, 100f);
        PrintRandom(n);
        return n;
    }
    public float GenerateSingleNumber(float min, float max)
    {
        float n = Random.Range(min, max);
        PrintRandom(n);
        return n;
    }

    public int GenerateSingleNumber(int min, int max)
    {
        int n = Random.Range(min, max);
        PrintRandom(n);
        return n;
    }
    public float GenerateSingleNumber(int seed, float min, float max)
    {
        SetSeed(seed);
        float n = Random.Range(min, max);
        PrintRandom(n);
        return n;
    }

    public void GenerateNNumbers(int seed, int noOfNumbers)
    {
        for (int i = 0; i < noOfNumbers; i++)
        {
            PrintRandom();
        }
    }
    public void GenerateNNumbers(int noOfNumbers)
    {
        for (int i = 0; i < noOfNumbers; i++)
        {
            PrintRandom();
        }
    }
    public void GenerateNNumbers()
    {
        SetSeed(seed);
        Random.InitState(seed);

        for (int i = 0; i < noOfNumbers; i++)
        {
            PrintRandom();
        }
    }

    private void Awake()
    {
        SetSeed(seed);
    }

    void PrintRandom()
    {
        if (debug)
        {
            Debug.Log(Random.Range(1f, 100f));
        }
    }
    void PrintRandom(float n)
    {
        if (debug)
        {
            Debug.Log(n);
        }
    }
}
