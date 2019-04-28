using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public float rotateSpeed;
    public bool rotate = false;
        
    void Update()
    {
        if (rotate)
        {
            transform.localEulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
        }
    }
}
