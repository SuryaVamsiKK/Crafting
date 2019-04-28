using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    public Transform[] planet;
    RaycastHit hit;
    Ray ray;
    public float m_LandingRange;
    public LayerMask m_Mask;
    public Vector3 target;
    public Vector3 targetUp;

    public bool landed = false;
    public bool exited = false;
    public Transform spwan;
    public GameObject cmLook;
    public GameObject playerDot;
    
    void Start()
    {
        target = Vector3.zero;
    }

    void Update()
    {
        ray = new Ray(transform.GetChild(0).position, -transform.up);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, m_LandingRange, m_Mask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                target = hit.point;
                targetUp = hit.normal;
                landed = !landed;
            }
        }

        if (landed)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.up = Vector3.Lerp(transform.up, targetUp, Time.deltaTime * 1f);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 0.5f);

            if(Input.GetKeyDown(KeyCode.F))
            {
                cmLook.SetActive(false);
                exited = true;
                GetComponent<Landing>().enabled = false;
                GetComponent<Movement>().enabled = false;
            }
        }

        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        Debug.DrawRay(ray.origin, -ray.direction * m_LandingRange, Color.red);
    }
}
