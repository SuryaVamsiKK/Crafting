using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageObject : MonoBehaviour
{
    public Mesh mesh;
    public string[] allowedBiomes;
    public Palettes[] palettes;

    public void OnValidate()
    {
        for (int i = 0; i < palettes.Length; i++)
        {
            if (palettes[i].palette.Length != transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length)
            {
                palettes[i].palette = new Color[transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length];
            }
        }
    }
}

public class Palettes
{
    public Color[] palette;
}