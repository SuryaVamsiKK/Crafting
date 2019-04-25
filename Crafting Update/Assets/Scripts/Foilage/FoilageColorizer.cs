using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoilageColorizer : MonoBehaviour
{
    public Color m_Primary;
    public Color m_Seconday;
    public Color m_Tertiary;
    RaycastHit hit;
    public LayerMask mask;
    public Transform planetCore;
    
    public float appearSpeed = 1.5f;
    float clip = 1f;

    bool set = false;

    private void Start()
    {
        transform.up = -(planetCore.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
    //    if (!set)
    //    {
    //        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f, mask))
    //        {
    //            transform.GetChild(0).gameObject.SetActive(true);
    //            transform.position = hit.point;
    //            set = true;
    //        }
    //        //else
    //        //{
    //        //    transform.GetChild(0).gameObject.SetActive(false);
    //        //}
    //    }
    }

    private void Update()
    {
        //transform.up = -(planetCore - transform.position).normalized;

        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if (clip > 0)
            {
                clip -= Time.deltaTime * appearSpeed;

                transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetFloat("_Clip", clip);
                if (transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetFloat("_Clip", clip);
                    if (transform.GetChild(0).GetComponent<MeshRenderer>().materials.Length > 2)
                    {
                        transform.GetChild(0).GetComponent<MeshRenderer>().materials[2].SetFloat("_Clip", clip);
                    }
                }
            }
        }
    }
    
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 50);
    //    Gizmos.DrawSphere(planetCore.position, 50);
    //}
}
