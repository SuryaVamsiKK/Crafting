using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    [Header("Clamps and Offsets")]
    float distance = 10f;
    public float offset = 0.25f;
    //public GameObject marker;

    [Header("Technical")]
    public Camera cam;
    public LayerMask mask;
    public float mass = 1f;

    [Header("Highlight")]
    [Range(0, 1)]public float outlineWidth = 0.4f;
    public Color outlineColor;

    public Transform outline;

    private void OnValidate()
    {
        outline.localScale = new Vector3(1 + (outlineWidth / 10f), 1 + (outlineWidth / 10f), 1 + (outlineWidth / 10f));
    }

    private void Start()
    {
        outline.GetComponent<MeshRenderer>().material.SetColor("_OutlineCol", outlineColor);
    }

    private void OnMouseDrag()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Collider>().enabled = false;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = cam.ScreenToWorldPoint(mousePos);

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 10f, mask))
        {
            if(hit.collider.transform.gameObject.tag == "Slot")
            {
                if (hit.collider.GetComponent<SpecificSlots>() == null || hit.collider.GetComponent<SpecificSlots>().slotTypeData.m_Resource == GetComponent<ResourceObject>().resourceType.m_Resource)
                {
                    transform.parent = hit.collider.transform;
                    objPos = hit.collider.transform.position;
                }
                else
                {
                    transform.parent = null;
                    objPos = hit.point + hit.normal * offset;
                    transform.up = hit.normal;
                }
            }
            else
            {
                transform.parent = null;
                objPos = hit.point + hit.normal * offset;
                transform.up = hit.normal;
            }
        }

        transform.position = Vector3.Lerp(transform.position, objPos, Time.deltaTime * 10f);


        if (this.gameObject.tag == "Resource")
        {
            if(transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            outline.gameObject.SetActive(true);
        }
        else
        {
            outline.gameObject.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = cam.ScreenToWorldPoint(mousePos);

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
        if (Physics.Raycast(ray, out hit, 10f, mask))
        {
            transform.parent = null;
            objPos = hit.point + hit.normal * offset;
            transform.up = hit.normal;
        }
    }

    private void OnMouseOver()
    {
        outline.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        outline.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        outline.gameObject.SetActive(false);
        //marker.transform.localPosition = Vector3.zero;
        //marker.transform.localRotation = Quaternion.identity;
        //marker.transform.localScale = new Vector3(1f, 1f, 1f);
        //marker.transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = true;
        if (transform.parent != null)
        {
            transform.parent.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
