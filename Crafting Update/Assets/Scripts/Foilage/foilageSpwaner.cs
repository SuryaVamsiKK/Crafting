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

    public void Spwan(Vector3 treePos, int biomeType, float elevationPoint)
    {
        int biomeIn = 0;
        string biomeName = "";

        for (int i = 0; i < Foilage.instance.biomeFoilage.Length; i++)
        {
            if (Foilage.instance.biomeFoilage[i].biomeName == GetComponent<MeshGenerator>().planetCore.GetComponent<ColorGenerator>().settings.biomeColorSettings.biomes[biomeType].BiomeName)
            {
                biomeIn = i;
                biomeName = GetComponent<MeshGenerator>().planetCore.GetComponent<ColorGenerator>().settings.biomeColorSettings.biomes[biomeType].BiomeName;
            }
        }

        planetCore = GetComponent<MeshGenerator>().planetCore;
        int choosenAsset = GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(0, reference.biomeFoilage[biomeIn].tree.Length);
        float fordebug = GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(1f, 1000f);
        
        if (fordebug > reference.biomeFoilage[biomeIn].tree[choosenAsset].spwanablility && elevationPoint > reference.biomeFoilage[biomeIn].tree[choosenAsset].height.minHeight && elevationPoint < reference.biomeFoilage[biomeIn].tree[choosenAsset].height.maxHeight) // to spwan or not
        {
            GameObject newTree = reference.getTree(choosenAsset, biomeName);
            if (newTree != null)
            {
                int randomPalette = 0;
                if (Foilage.instance.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes.Length > 1)
                {
                    randomPalette = Random.Range(0, Foilage.instance.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes.Length);
                }

                newTree.transform.parent = this.transform;
                //Debug.Log(treePos);
                newTree.transform.localPosition = treePos;
                //newTree.transform.up = -(planetCore.position - newTree.transform.position).normalized;
                newTree.GetComponent<FoilageColorizer>().planetCore = planetCore;

                #region Palette based Color Generation
                               
                if(newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length == 1)
                {
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].primary);
                }
                if (newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length == 2)
                {
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].primary);
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].tertiary);
                }
                if (newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length == 3)
                {
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].primary);
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].secondary);
                    newTree.transform.GetChild(0).GetComponent<MeshRenderer>().materials[2].SetColor("_Color", reference.biomeFoilage[biomeIn].tree[choosenAsset].colorPalettes[randomPalette].tertiary);
                }
                #endregion

            }
        }
    }
}
