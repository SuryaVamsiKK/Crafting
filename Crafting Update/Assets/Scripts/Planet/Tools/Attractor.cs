using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 667.4f;

    public Rigidbody rb;
    public bool attract = false;

    void FixedUpdate()
    {
        Rigidbody[] objects = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in objects)
        {
            if (rb != this.rb)
            {
                if (attract)
                {
                    Attract(rb);
                }
            }
        }
    }

    void Attract(Rigidbody objToAttract)
    {
        Vector3 direction = rb.position - objToAttract.position;
        float disance = direction.magnitude;

        float forceMagnitude = (rb.mass * objToAttract.mass) / Mathf.Pow(disance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        objToAttract.AddForce(force, ForceMode.Acceleration);
    }

}
