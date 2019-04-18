using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData : MonoBehaviour
{
    void Update()
    {
        if (transform.parent == null)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
