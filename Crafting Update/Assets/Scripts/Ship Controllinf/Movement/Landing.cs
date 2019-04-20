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
    Vector3 targ;

    public bool landed = false;
    void Start()
    {
        target = Vector3.zero;
    }

    void Update()
    {
        if (planet.Length > 1)
        {
            if (Vector3.Distance(planet[0].position, transform.position) < Vector3.Distance(planet[1].position, transform.position))
            {
                targ = planet[0].position;
            }
            else
            {
                targ = planet[1].position;
            }
        }

        ray = new Ray(transform.position, (transform.position - targ));

        if (Physics.Raycast(ray.origin, -ray.direction, out hit, m_LandingRange, m_Mask))
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
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        Debug.DrawRay(ray.origin, -ray.direction * m_LandingRange, Color.red);
    }
}
