using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public Resource resourceType;
    public float appearSpeed = 1.5f;
    float clip = 1f;

    private void Start()
    {
        this.gameObject.name = resourceType.m_Resource.ToString();
        GetComponent<MeshRenderer>().material.SetColor("_Color", resourceType.m_ColorCode);
        GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", resourceType.smoothness);
        GetComponent<MeshRenderer>().material.SetFloat("_Metallic", resourceType.metallic);
    }

    private void Update()
    {
        if (clip > 0)
        {
            clip -= Time.deltaTime * appearSpeed;
            GetComponent<MeshRenderer>().material.SetFloat("_Clip", clip);
        }
    }
}
