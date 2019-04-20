using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public float rotateSpeed;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
    }
}
