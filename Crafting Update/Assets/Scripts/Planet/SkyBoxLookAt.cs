using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxLookAt : MonoBehaviour
{
    public Transform sun;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.LookAt(sun);
    }
}
