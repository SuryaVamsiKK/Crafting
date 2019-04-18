using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceCollection : MonoBehaviour
{
    [Header("Design")]
    public GameObject resource;
    public int resetValue = 1;
    public GameObject marker;

    [Header("Gun")]
    public float gunPower;
    public float gunDistance;
    public GameObject Gun;
    public Material red;
    public Material green;
    [Space]

    [Header("Resource")]
    [ReadOnly] public Resource resourceOnGun;
    public amountHolder[] storedValues;

    [Header("Technical")]
    public Camera cam;
    public LayerMask mask;
    public LayerMask highlightermask;

    bool extractionMode = false;

    private void Start()
    {
        for (int i = 0; i < storedValues.Length; i++)
        {
            storedValues[i].amount = resetValue;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            extractionMode = !extractionMode;
        }

        if (extractionMode)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                mouseToObject();
            }
            Gun.transform.GetChild(0).localPosition = Vector3.Lerp(Gun.transform.GetChild(0).localPosition, Gun.transform.GetChild(1).localPosition, Time.deltaTime * 10f);
            Gun.transform.GetChild(0).localScale = Vector3.Lerp(Gun.transform.GetChild(0).localScale, Gun.transform.GetChild(1).localScale, Time.deltaTime * 10f);
            Gun.transform.GetChild(0).localRotation = Quaternion.Lerp(Gun.transform.GetChild(0).localRotation, Gun.transform.GetChild(1).localRotation, Time.deltaTime * 10f);
        }
        else
        {
            Gun.transform.GetChild(0).localPosition = Vector3.Lerp(Gun.transform.GetChild(0).localPosition, Gun.transform.GetChild(2).localPosition, Time.deltaTime * 10f);
            Gun.transform.GetChild(0).localScale = Vector3.Lerp(Gun.transform.GetChild(0).localScale, Gun.transform.GetChild(2).localScale, Time.deltaTime * 10f);
            Gun.transform.GetChild(0).localRotation = Quaternion.Lerp(Gun.transform.GetChild(0).localRotation, Gun.transform.GetChild(2).localRotation, Time.deltaTime * 10f);
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        if (Physics.Raycast(ray, out hit, gunDistance, highlightermask))
        {
            if (!extractionMode)
            {
                marker.transform.GetChild(0).gameObject.SetActive(true);
                marker.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                marker.transform.GetChild(2).gameObject.SetActive(true);
                marker.transform.GetChild(0).gameObject.SetActive(false);
                if(hit.collider.gameObject.tag != "ResourceNodes")
                {
                    marker.transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().material = red;
                }
                else
                {
                    marker.transform.GetChild(2).GetChild(1).GetComponent<MeshRenderer>().material = green;
                }
            }
            marker.transform.position = hit.point;
            marker.transform.up = hit.normal;
            if (extractionMode)
            {
                //Gun.transform.GetChild(0).LookAt(marker.transform);
            }
        }
        else
        {
            marker.transform.GetChild(0).gameObject.SetActive(false);
            marker.transform.GetChild(2).gameObject.SetActive(false);
        }


        for (int i = 0; i < storedValues.Length; i++)
        {
            if (storedValues[i].amount <= 0)
            {
                if (checkAllAreTrue())
                {
                    GameObject g = Instantiate(resource, Gun.transform.GetChild(0).GetChild(0).position, Gun.transform.GetChild(0).GetChild(0).rotation);
                    g.GetComponent<ResourceObject>().resourceType = resourceOnGun;
                    g.GetComponent<ClickAndDrag>().cam = cam;
                    //g.GetComponent<ClickAndDrag>().marker = marker;
                }
                else
                {

                    for (int j = 0; j < transform.childCount; j++)
                    {
                        if (!transform.GetChild(j).GetComponent<Inventory>().taken && (hit.collider.GetComponent<SpecificSlots>() == null || hit.collider.GetComponent<SpecificSlots>().slotTypeData.m_Resource == GetComponent<ResourceObject>().resourceType.m_Resource))
                        {

                            GameObject g = Instantiate(resource, Gun.transform.GetChild(0).GetChild(0).position, Gun.transform.GetChild(0).GetChild(0).rotation);
                            g.GetComponent<ResourceObject>().resourceType = resourceOnGun;
                            g.GetComponent<ClickAndDrag>().cam = cam;
                            //g.GetComponent<ClickAndDrag>().marker = marker;
                            g.GetComponent<Rigidbody>().isKinematic = true;
                            g.GetComponent<Rigidbody>().freezeRotation = true;
                            g.GetComponent<Collider>().isTrigger = true;
                            g.transform.parent = transform.GetChild(j);
                            g.transform.parent.GetComponent<Collider>().enabled = false;
                            break;
                        }
                    }
                }

                storedValues[i].amount = resetValue;
            }
        }

    }

    void mouseToObject()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, gunDistance, mask))
        {
            if(hit.transform.gameObject.tag == "ResourceNodes")
            {
                resourceOnGun = hit.transform.GetComponent<ResourceNode>().resource;
                for (int i = 0; i < storedValues.Length; i++)
                {
                    if(storedValues[i].resource == hit.transform.GetComponent<ResourceNode>().resource)
                    {
                        storedValues[i].amount -= Time.deltaTime * gunPower;
                    }
                }
            }
            else if(hit.transform.gameObject.tag != "Resource")
            {
                marker.transform.position = hit.point;
                marker.transform.up = hit.normal;
            }
        }
    }

    [System.Serializable]
    public struct amountHolder
    {
        public Resource resource;
        [ReadOnly] public float amount;
    }

    bool checkAllAreTrue()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Inventory>().taken)
            {
                return false;
            }
        }

        return true;
    }
}
