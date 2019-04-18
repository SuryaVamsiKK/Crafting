using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foilageSpwaner : MonoBehaviour
{
    public bool spwanTrees;
    Transform planetCore;

    Foilage reference;

    private void Awake()
    {
        reference = Foilage.instance;
    }

    // Start is called before the first frame update
    /*void test()
    {
        planetCore = GetComponent<MeshGenerator>().planetCore;

        if (spwanTrees)
        {
            planetCore.GetComponent<Randomizer>().SetSeed(planetCore.GetComponent<Randomizer>().seed * 1000 + (Mathf.RoundToInt((planetCore.InverseTransformPoint(transform.position).x * 100) + (planetCore.InverseTransformPoint(transform.position).y * 10) + (planetCore.InverseTransformPoint(transform.position).z))));
            for (int i = 0; i < GetComponent<MeshGenerator>().spwanAblepoints.Count; i++)
            {
                int choosenAsset = GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(0, GameObject.FindObjectOfType<Foilage>().tree.Length);
                if (GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(1f, 1000f) > GameObject.FindObjectOfType<Foilage>().spwanablility[choosenAsset]) // to spwan or not
                {
                    GameObject newTree = GameObject.FindObjectOfType<Foilage>().getTree(choosenAsset);
                    if (newTree != null)
                    {
                        Vector3 treePos = GetComponent<MeshGenerator>().spwanablePosition[i];
                        newTree.transform.parent = this.transform;
                        newTree.transform.position = planetCore.TransformPoint(treePos);

                        #region Palette based Color Generation
                        
                        int randomPallete = 0;
                        if (GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[GetComponent<MeshGenerator>().biometype[i]].colorPalettes.Count > 1)
                        {
                            randomPallete = Random.Range(0, GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[GetComponent<MeshGenerator>().biometype[i]].colorPalettes.Count);
                        }
                        
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[GetComponent<MeshGenerator>().biometype[i]].colorPalettes[randomPallete].primary);
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetFloat("_Smoothness", 0f);
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetColor("_BaseColor", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[GetComponent<MeshGenerator>().biometype[i]].colorPalettes[randomPallete].secondary);
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetFloat("_Smoothness", 0f);
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[2].SetColor("_BaseColor", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[GetComponent<MeshGenerator>().biometype[i]].colorPalettes[randomPallete].tertiary);
                        newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[2].SetFloat("_Smoothness", 0f);

                        #endregion

                        newTree.transform.up = -(planetCore.position - transform.position).normalized;
                    }
                }
            }
            GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().SetSeed(GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().seed);
        }
    }*/

    public void Spwan(Vector3 treePos, int biomeType)
    {
        planetCore = GetComponent<MeshGenerator>().planetCore;
        int choosenAsset = GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(0, reference.tree.Length);
        float fordebug = GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(1f, 1000f);
        if (fordebug > reference.spwanablility[choosenAsset]) // to spwan or not
        {
            GameObject newTree = reference.getTree(choosenAsset);
            if (newTree != null)
            {
                newTree.transform.parent = this.transform;
                //Debug.Log(treePos);
                newTree.transform.position = treePos + planetCore.position;
                newTree.transform.up = -(planetCore.position - transform.position).normalized;

                #region Palette based Color Generation

                int randomPallete = 0;
                if (GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[biomeType].colorPalettes.Count > 1)
                {
                    randomPallete = Random.Range(0, GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[biomeType].colorPalettes.Count);
                }

                newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetColor("_Color", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[biomeType].colorPalettes[randomPallete].primary);
                newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetColor("_Color", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[biomeType].colorPalettes[randomPallete].secondary);
                newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[2].SetColor("_Color", GetComponent<MeshGenerator>().colorGenerator.settings.biomeColorSettings.biomes[biomeType].colorPalettes[randomPallete].tertiary);

                #endregion

            }
        }
    }

}
