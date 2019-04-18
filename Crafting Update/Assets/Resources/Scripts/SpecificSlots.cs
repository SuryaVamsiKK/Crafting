using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificSlots : MonoBehaviour
{
    public Resource slotTypeData;
    public Color recievedColor;

    public void Update()
    {
        if(GetComponent<Inventory>().taken)
        {
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", recievedColor);
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", recievedColor);
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", slotTypeData.m_ColorCode);
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", slotTypeData.m_ColorCode);
        }
    }
}
