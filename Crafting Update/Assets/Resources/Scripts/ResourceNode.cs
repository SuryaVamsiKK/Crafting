using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public float life = 100f;
    public Resource resource;
    public bool extracting;
    
    private void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", resource.m_ColorCode);
        GetComponent<MeshRenderer>().material.SetFloat("_Clip", 0);
    }

    private void Update()
    {
        if(extracting)
        {
            life -= 1 * Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        extracting = true;
    }

    private void OnMouseExit()
    {
        extracting = false;
    }
}
