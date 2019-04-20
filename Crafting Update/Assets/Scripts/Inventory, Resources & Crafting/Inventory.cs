using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool taken = false;
    [ColorUsageAttribute(true, true)] public Color baseColor = Color.white;
    
    void Update()
    {
        if (transform.childCount > 1)
        {
            taken = true;
            transform.GetChild(1).localPosition = Vector3.Lerp(transform.GetChild(1).localPosition, Vector3.zero, Time.deltaTime * 10f);
            transform.GetChild(1).localRotation = Quaternion.Lerp(transform.GetChild(1).localRotation, Quaternion.identity, Time.deltaTime * 10f);
            //transform.GetChild(1).localPosition = Vector3.zero;
            //transform.GetChild(1).localRotation = Quaternion.identity;
            //GetComponent<Collider>().enabled = false;
        }
        else
        {
            GetComponent<Collider>().enabled = true;
            taken = false;
        }

        if (GetComponent<SpecificSlots>() == null)
        {
            if (taken)
            {
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", transform.GetChild(1).GetComponent<ResourceObject>().resourceType.m_ColorCode);
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", transform.GetChild(1).GetComponent<ResourceObject>().resourceType.m_ColorCode);
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", baseColor);
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", baseColor);
            }
        }       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 0.125f);
    }
}
