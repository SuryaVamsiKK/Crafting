﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class QuadTreePlanetry : MonoBehaviour
{
    [HideInInspector] public float threshold = 2f;
    public bool divide = false;
    [HideInInspector] public bool recreate = false;
    public bool revertToHigherLOD = false;
    [HideInInspector] public int maxDepth = 0;
    //GameObject Foilge;

    bool divided = false;
    

    void Start()
    {
        if(transform.parent.GetComponent<QuadTreePlanetry>() != null)
        {
            maxDepth = transform.parent.GetComponent<QuadTreePlanetry>().maxDepth - 1;
            threshold = transform.parent.GetComponent<QuadTreePlanetry>().threshold / 2;
        }
        else
        {
            maxDepth = transform.parent.GetComponent<PlanetGenerator>().lodSettings.maxDepth;
            threshold = transform.parent.GetComponent<PlanetGenerator>().shapeSettings.planetRadius + transform.parent.GetComponent<PlanetGenerator>().lodSettings.playerdetectionRadius;
        }

        //if(maxDepth == 0)
        //{
            //Foilge = GameObject.FindObjectOfType<Foilage>().gameObject;
            //Foilge.transform.parent = GetComponent<MeshGenerator>().planetCore.transform.parent;
        //}
    }

    void Update()
    {
        
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < threshold)
        {
            if (maxDepth > 0)
            {
                divide = true;
                if (!divided)
                {
                    quadTree();
                    //StartCoroutine(div());
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(RunOnThreadPool));
                    divided = true;
                }
            }
        }
        else
        {
            if(maxDepth == 0)
            {
                //if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.parent.position) > transform.parent.GetComponent<QuadTreePlanetry>().threshold)
                //{
                //    GetComponent<foilageSpwaner>().Reseter();
                //}
                goto lb; 
            }

            divided = false;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                    GetComponent<MeshRenderer>().enabled = true;
                }
            }
            lb:;
        }
    }

    private void OnValidate()
    {
        quadTree();
    }

    void quadTree()
    {
        if(divide)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            for (int i = 0; i < 4; i++)
            {             
                GameObject g = GameObject.Instantiate(GetComponent<MeshGenerator>().chunk, transform);
                g.GetComponent<MeshGenerator>().lod = GetComponent<MeshGenerator>().lod + 1;
                g.name = "LOD : " + g.GetComponent<MeshGenerator>().lod;
                g.GetComponent<MeshGenerator>().mat = GetComponent<MeshGenerator>().mat;
                
                MeshGenerator g_MeshGenerator =  g.GetComponent<MeshGenerator>();
                g_MeshGenerator.quad_Location = i;
                g_MeshGenerator.chunk = GetComponent<MeshGenerator>().chunk;
                g_MeshGenerator.localUp = GetComponent<MeshGenerator>().localUp;
                g_MeshGenerator.shapeSettings = GetComponent<MeshGenerator>().shapeSettings;
                g_MeshGenerator.colorGenerator = GetComponent<MeshGenerator>().colorGenerator;
                g_MeshGenerator.CreateShape();
                g_MeshGenerator.UpdateMesh(); 
            }
            divide = false;
        }
    }

    private void RunOnThreadPool(object state)
    {
        quadTree();
    }

    public IEnumerator div()
    {
        yield return new WaitForSeconds(0);
        quadTree();
    }
}
